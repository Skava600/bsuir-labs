-- first
SET SERVEROUTPUT ON; 
CREATE TABLE COMPANIES
(
    id           NUMBER UNIQUE NOT NULL,
    name         VARCHAR2(50)  NOT NULL,
    date_founded DATE          NOT NULL
);

CREATE TABLE PLATOONS
(
    id         NUMBER UNIQUE NOT NULL,
    name       VARCHAR2(50)  NOT NULL,
    company_id NUMBER        NOT NULL,
    CONSTRAINT pp_pc FOREIGN KEY (company_id) REFERENCES COMPANIES (id) ON DELETE CASCADE
);

CREATE TABLE SOLDIERS
(
    id       NUMBER UNIQUE NOT NULL,
    name     VARCHAR2(50)  NOT NULL,
    platoon_id NUMBER        NOT NULL,
    CONSTRAINT sk_pl FOREIGN KEY (platoon_id) REFERENCES PLATOONS (id) ON DELETE CASCADE
);

-- second
CREATE TABLE LOG_COMPANIES
(
    operation    VARCHAR(10) NOT NULL,
    id           NUMBER,
    old_name     VARCHAR(50),
    new_name     VARCHAR(50),
    date_founded DATE,
    tm           TIMESTAMP   NOT NULL
);

CREATE TABLE LOG_PLATOONS
(
    operation  VARCHAR(10) NOT NULL,
    id         NUMBER,
    old_name   VARCHAR(50),
    new_name   VARCHAR(50),
    company_id NUMBER,
    tm         TIMESTAMP   NOT NULL
);

CREATE TABLE LOG_SOLDIERS
(
    operation VARCHAR(10) NOT NULL,
    id        NUMBER,
    old_name  VARCHAR(50),
    new_name  VARCHAR(50),
    platoon_id  NUMBER,
    tm        TIMESTAMP   NOT NULL
);
/
CREATE OR REPLACE TRIGGER LOG_FC_INS
    AFTER INSERT
    ON COMPANIES
    FOR EACH ROW
BEGIN
    INSERT INTO LOG_COMPANIES(operation, id, new_name, old_name, date_founded, tm)
    VALUES ('INSERT', :new.id, NULL, NULL, NULL, current_timestamp);
END;
/
CREATE OR REPLACE TRIGGER LOG_FC_UPD
    AFTER UPDATE
    ON COMPANIES
    FOR EACH ROW
BEGIN
    INSERT INTO LOG_COMPANIES(operation, id, new_name, old_name, date_founded, tm)
    VALUES ('UPDATE', :new.id, :new.name, :old.name, NULL, current_timestamp);
END;
/
CREATE OR REPLACE TRIGGER LOG_FC_DEL
    AFTER DELETE
    ON COMPANIES
    FOR EACH ROW
BEGIN
    INSERT INTO LOG_COMPANIES(operation, id, new_name, old_name, date_founded, tm)
    VALUES ('DELETE', :old.id, :old.name, NULL, :old.date_founded, current_timestamp);
END;
/
-- log PLATOONS

CREATE OR REPLACE TRIGGER LOG_GR_INS
    AFTER INSERT
    ON PLATOONS
    FOR EACH ROW
BEGIN
    INSERT INTO LOG_PLATOONS(operation, id, new_name, old_name, company_id, tm)
    VALUES ('INSERT', :new.id, NULL, NULL, NULL, current_timestamp);
END;
/
CREATE OR REPLACE TRIGGER LOG_GR_UPD
    AFTER UPDATE
    ON PLATOONS
    FOR EACH ROW
BEGIN
    INSERT INTO LOG_PLATOONS(operation, id, new_name, old_name, company_id, tm)
    VALUES ('UPDATE', :new.id, :new.name, :old.name, NULL, current_timestamp);
END;
/
CREATE OR REPLACE TRIGGER LOG_GR_DEL
    AFTER DELETE
    ON PLATOONS
    FOR EACH ROW
BEGIN
    INSERT INTO LOG_PLATOONS(operation, id, new_name, old_name, company_id, tm)
    VALUES ('DELETE', :old.id, :old.name, NULL, :old.company_id, current_timestamp);
END;
/
--log SOLDIERS

CREATE OR REPLACE TRIGGER LOG_ST_INS
    AFTER INSERT
    ON SOLDIERS
    FOR EACH ROW
BEGIN
    INSERT INTO LOG_SOLDIERS(operation, id, new_name, old_name, platoon_id, tm)
    VALUES ('INSERT', :new.id, NULL, NULL, NULL, current_timestamp);
END;
/
CREATE OR REPLACE TRIGGER LOG_ST_UPD
    AFTER UPDATE
    ON SOLDIERS
    FOR EACH ROW
BEGIN
    INSERT INTO LOG_SOLDIERS(operation, id, new_name, old_name, platoon_id, tm)
    VALUES ('UPDATE', :new.id, :new.name, :old.name, NULL, current_timestamp);
END;
/
CREATE OR REPLACE TRIGGER LOG_ST_DEL
    AFTER DELETE
    ON SOLDIERS
    FOR EACH ROW
BEGIN
    INSERT INTO LOG_SOLDIERS(operation, id, new_name, old_name, platoon_id, tm)
    VALUES ('DELETE', :old.id, :old.name, NULL, :old.platoon_id, current_timestamp);
END;
/

CREATE OR REPLACE PROCEDURE RESTORE_DATA(point_time TIMESTAMP)
    IS
    PROCEDURE RESTORE_COMPANIES_TIME(point_time TIMESTAMP)
        IS
    BEGIN
        FOR res IN (SELECT * FROM LOG_COMPANIES WHERE tm >= point_time order by tm desc)
            LOOP
                IF res.operation = 'INSERT' THEN
                    DELETE FROM COMPANIES WHERE id = res.id;
                ELSIF res.operation = 'UPDATE' THEN
                    UPDATE COMPANIES SET name=res.old_name WHERE id = res.id;
                ELSE
                    INSERT INTO COMPANIES(id, name, date_founded) VALUES (res.id, res.new_name, res.date_founded);
                END IF;
            END LOOP;
        DELETE FROM LOG_COMPANIES WHERE tm >= point_time;
    END RESTORE_COMPANIES_TIME;

    PROCEDURE RESTORE_PLATOONS_TIME(point_time TIMESTAMP)
        IS
    BEGIN
        FOR res IN (SELECT * FROM LOG_PLATOONS WHERE tm >= point_time order by tm desc)
            LOOP
                IF res.operation = 'INSERT' THEN
                    DELETE FROM PLATOONS WHERE id = res.id;
                ELSIF res.operation = 'UPDATE' THEN
                    UPDATE PLATOONS SET name=res.old_name WHERE id = res.id;
                ELSE
                    INSERT INTO PLATOONS(id, name, company_id) VALUES (res.id, res.new_name, res.company_id);
                END IF;
            END LOOP;
        DELETE FROM LOG_PLATOONS WHERE tm >= point_time;
    END RESTORE_PLATOONS_TIME;

    PROCEDURE RESTORE_SOLDIERS_TIME(point_time TIMESTAMP)
        IS
    BEGIN
        FOR res IN (SELECT * FROM LOG_SOLDIERS WHERE tm >= point_time order by tm desc)
            LOOP
                IF res.operation = 'INSERT' THEN
                    DELETE FROM SOLDIERS WHERE id = res.id;
                ELSIF res.operation = 'UPDATE' THEN
                    UPDATE SOLDIERS SET name=res.old_name WHERE id = res.id;
                ELSE
                    INSERT INTO SOLDIERS(id, name, platoon_id) VALUES (res.id, res.new_name, res.platoon_id);
                END IF;
            END LOOP;
        DELETE FROM LOG_SOLDIERS WHERE tm >= point_time;
    END RESTORE_SOLDIERS_TIME;
BEGIN
    RESTORE_SOLDIERS_TIME(point_time);
    RESTORE_PLATOONS_TIME(point_time);
    RESTORE_COMPANIES_TIME(point_time);
END RESTORE_DATA;
/
--html

CREATE TABLE REPORTS
(
    table_name         VARCHAR2(50) NOT NULL,
    request_type       VARCHAR2(50) NOT NULL,
    number_of_requests NUMBER       NOT NULL,
    tm                 TIMESTAMP
);

CREATE OR REPLACE PROCEDURE CREATE_REPORT(point_time TIMESTAMP)
    IS
    last_report TIMESTAMP;
    code        VARCHAR2(32000);
BEGIN
    IF point_time is NULL THEN
        SELECT max(tm) INTO last_report FROM REPORTS;
        IF last_report is NULL THEN
            last_report := current_timestamp;
        END IF;
    ELSE
        last_report := point_time;
    END IF;

    code := '
<!DOCTYPE html>
	<html lang="en">
	<head>
		<meta charset="UTF-8">
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
		<title>Report</title>
        <style>
            td, th {
                border: 1px solid #dddddd;
                text-align: left;
                padding: 8px;
            }
        </style>
	</head>
	<body>
        <h4>Faculty table</h4>
        <table>
            <tr>
                <th>Request type</th>
                <th>Number of requests</th>
            </tr>';
    FOR res IN (SELECT operation, count(operation) as numberof
                FROM LOG_COMPANIES
                WHERE tm >= last_report
                group by operation)
        LOOP
            code := code || '<tr>
                                <td>' || res.operation || '</td>
                                <td>' || res.numberof || '</td>
                            </tr>';
            INSERT INTO REPORTS VALUES ('LOG_COMPANIES', res.operation, res.numberof, current_timestamp);
        END LOOP;


    code := code || '</table>
                     <h4>Group table</h4>
                     <table>
                     <tr>
                        <th>Request type</th>
                        <th>Number of requests</th>
                     </tr>';
    FOR res IN (SELECT operation, count(operation) as numberof
                FROM LOG_PLATOONS
                WHERE tm >= last_report
                group by operation)
        LOOP
            code := code || '<tr>
                                <td>' || res.operation || '</td>
                                <td>' || res.numberof || '</td>
                            </tr>';

            INSERT INTO REPORTS VALUES ('LOG_COMPANIES', res.operation, res.numberof, current_timestamp);
        END LOOP;

    code := code || '</table>
                     <h4>Student table</h4>
                     <table>
                     <tr>
                        <th>Request type</th>
                        <th>Number of requests</th>
                     </tr>';

    FOR res IN (SELECT operation, count(operation) as numberof
                FROM LOG_SOLDIERS
                WHERE tm >= last_report
                group by operation)
        LOOP
            code := code || '<tr>
                                <td>' || res.operation || '</td>
                                <td>' || res.numberof || '</td>
                            </tr>';

            INSERT INTO REPORTS VALUES ('LOG_COMPANIES', res.operation, res.numberof, current_timestamp);
        END LOOP;

    code := code || '</table>
                     </body>
                     </html>';
    DBMS_OUTPUT.PUT_LINE(code);
END;
/
INSERT INTO COMPANIES VALUES (1, 'kastus', TO_DATE( '01 Jan 2017', 'DD MON YYYY' ));
/
BEGIN

INSERT INTO PLATOONS VALUES (1, '1', 1);
INSERT INTO PLATOONS VALUES (2, '2', 1);
INSERT INTO SOLDIERS VALUES(1, 'Vlad', 1);
INSERT INTO SOLDIERS VALUES (2, 'Fedya', 1);
INSERT INTO SOLDIERS VALUES (3, 'Stas', 2);
INSERT INTO SOLDIERS VALUES (4, 'Kirill', 1);
DELETE FROM SOLDIERS WHERE ID = 3;
END;
/
BEGIN
    CREATE_REPORT(to_timestamp('2021', 'yyyy'));
    RESTORE_DATA(to_timestamp('2022-05-25 12:16:40', 'yyyy-mm-dd hh24-mi-ss'));
   
end;
/

