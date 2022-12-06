grant all privileges to C##SKAVA;
DROP USER C##PROD CASCADE;
DROP USER C##DEV CASCADE;

CREATE USER C##PROD IDENTIFIED BY "123";
CREATE USER C##DEV IDENTIFIED BY "123";

GRANT ALL PRIVILEGES TO C##PROD;
GRANT ALL PRIVILEGES TO C##DEV;
/
CREATE TABLE prod.products 
(product_id NUMBER(10) CONSTRAINT products_pk PRIMARY KEY,
product_name VARCHAR2(50) NOT NULL,
category VARCHAR2(50));

CREATE TABLE dev.products
(product_id NUMBER(10) CONSTRAINT products_pk PRIMARY KEY,
product_name VARCHAR2(50) NOT NULL,
category VARCHAR2(50));

/
CREATE OR REPLACE PROCEDURE dev.proc(offset IN INTERVAL DAY TO SECOND)
IS
    counter number;
BEGIN
    counter:=1;
END proc;
/

SET SERVEROUTPUT ON;
CREATE OR REPLACE PROCEDURE COMPARE_SCHEMAS(
dev_schema_name VARCHAR2, prod_schema_name VARCHAR2)
IS
    counter NUMBER;
    counter2 NUMBER;
    text VARCHAR2(100);
BEGIN
    dbms_output.put_line('Comparing 2 schemes, printing difference in tables');
    -- TO CREATE IN PROD
    FOR res IN (SELECT DISTINCT  table_name FROM all_tab_columns WHERE OWNER = dev_schema_name AND (table_name, column_name)
    NOT IN (SELECT table_name, column_name FROM all_tab_columns WHERE OWNER = prod_schema_name))
        LOOP
            counter :=0;
            SELECT COUNT(*) INTO counter FROM all_tables WHERE OWNER = prod_schema_name AND table_name = res.table_name;
            IF counter > 0 THEN
                FOR res2 IN (SELECT DISTINCT column_name, data_type FROM all_tab_columns WHERE OWNER = dev_schema_name AND table_name = res.table_name  AND (table_name, column_name) not in
                            (SELECT table_name, column_name FROM all_tab_columns WHERE OWNER = prod_schema_name))
                             LOOP
                                DBMS_OUTPUT.PUT_LINE('ALTER TABLE ' || prod_schema_name || '.' || res.table_name || ' ADD ' || res2.column_name || ' ' || res2.data_type || ';');
                            END LOOP;
            ELSE
                DBMS_OUTPUT.PUT_LINE('CREATE TABLE ' || prod_schema_name || '.' || res.table_name || ' AS (SELECT * FROM ' || res.table_name || ' WHERE 1=0);');
            END IF;
        END LOOP;
     -- prod tables to delete or drop columns
    FOR res IN (SELECT  DISTINCT table_name FROM all_tab_columns WHERE OWNER = prod_schema_name  AND (table_name, column_name) NOT IN
            (SELECT table_name, column_name FROM all_tab_columns WHERE OWNER = dev_schema_name))
        LOOP
            counter := 0;
            counter2 :=0;
            SELECT COUNT(column_name) INTO counter FROM all_tab_columns WHERE OWNER = prod_schema_name AND table_name = res.table_name;
            SELECT COUNT(column_name) INTO counter2 FROM all_tab_columns WHERE OWNER = dev_schema_name AND table_name = res.table_name;
            IF counter != counter2 THEN
                FOR res2 IN (SELECT column_name FROM all_tab_columns WHERE OWNER = prod_schema_name AND table_name = res.table_name and
                                column_name NOT IN (SELECT column_name FROM all_tab_columns WHERE OWNER = dev_schema_name AND table_name = res.table_name))
                            LOOP
                                DBMS_OUTPUT.PUT_LINE('ALTER TABLE '|| prod_schema_name || '.' || res.table_name || ' DROP COLUMN ' || res2.column_name || ';');
                            END LOOP;
            ELSE
                DBMS_OUTPUT.PUT_LINE('DROP TABLE ' || prod_schema_name || '.' || res.table_name || ' CASCADE CONSTRAINTS;');
            END IF;
        END LOOP;

    -- dev procedures to create in prod
    FOR res IN (SELECT DISTINCT object_name FROM all_objects WHERE object_type='PROCEDURE' AND OWNER=dev_schema_name  AND object_name NOT IN
            (SELECT object_name FROM all_objects WHERE OWNER = prod_schema_name AND object_type='PROCEDURE'))
        LOOP
            counter := 0;
            DBMS_OUTPUT.PUT_LINE('CREATE OR REPLACE ');
            FOR res2 IN (SELECT text FROM all_source where type='PROCEDURE' AND NAME=res.object_name AND OWNER=dev_schema_name)
                LOOP
                    IF COUNTER != 0 THEN
                        DBMS_OUTPUT.PUT_LINE(rtrim(res2.text,chr (10) || chr (13)));
                    ELSE
                       DBMS_OUTPUT.PUT_LINE(rtrim(prod_schema_name || '.' || res2.text,chr (10) || chr (13)));
                       counter := 1;
                    END IF;
                END LOOP;
        END LOOP;

    -- prod procedures to delete

    FOR res IN (SELECT DISTINCT object_name FROM all_objects WHERE object_type='PROCEDURE' AND OWNER=prod_schema_name AND object_name NOT IN
            (SELECT object_name FROM all_objects WHERE OWNER = dev_schema_name AND object_type='PROCEDURE'))
        LOOP
            DBMS_OUTPUT.PUT_LINE('DROP PROCEDURE ' || prod_schema_name || '.' || res.object_name);
        END LOOP;

    --dev functions to create in prod
    FOR res IN (SELECT DISTINCT object_name FROM all_objects WHERE object_type='FUNCTION' AND OWNER=dev_schema_name AND object_name NOT IN
            (SELECT object_name FROM all_objects WHERE OWNER = prod_schema_name AND object_type='FUNCTION'))
        LOOP
            counter := 0;
            DBMS_OUTPUT.PUT_LINE('CREATE OR REPLACE ');
            FOR res2 IN (SELECT text FROM all_source WHERE TYPE='FUNCTION' AND NAME=res.object_name AND OWNER=dev_schema_name)
                LOOP
                    IF COUNTER != 0 THEN
                        DBMS_OUTPUT.PUT_LINE(rtrim(res2.text,chr (10) || chr (13)));
                    ELSE
                       DBMS_OUTPUT.PUT_LINE(rtrim(prod_schema_name || '.' || res2.text,chr (10) || chr (13)));
                       counter := 1;
                    END IF;
                END LOOP;
        END LOOP;

    --prod functions to delete
    FOR res IN (SELECT DISTINCT object_name FROM all_objects WHERE object_type='FUNCTION' AND OWNER =prod_schema_name AND object_name NOT IN
            (SELECT object_name FROM all_objects WHERE OWNER = dev_schema_name AND object_type='FUNCTION'))
        LOOP
            DBMS_OUTPUT.PUT_LINE('DROP FUNCTION ' || prod_schema_name || '.' || res.object_name);
        END LOOP;

    --dev indexes to create in prod
    FOR res IN (SELECT  index_name, index_type, table_name FROM all_indexes WHERE table_owner=dev_schema_name AND index_name NOT LIKE '%_PK' AND index_name NOT IN
            (SELECT index_name FROM all_indexes WHERE table_owner=prod_schema_name AND index_name NOT LIKE '%_PK'))
        LOOP
            SELECT column_name INTO text FROM ALL_IND_COLUMNS WHERE index_name=res.index_name AND table_owner=dev_schema_name;
            DBMS_OUTPUT.PUT_LINE('CREATE ' || res.index_type || ' INDEX ' || res.index_name || ' ON ' || prod_schema_name || '.' || res.table_name || '(' || text || ');');
        END LOOP;

    --delete indexes drom prod
    FOR res IN (SELECT  index_name FROM all_indexes WHERE table_owner= prod_schema_name  AND index_name NOT LIKE '%_PK' AND index_name NOT IN
            (SELECT index_name FROM all_indexes WHERE table_owner=dev_schema_name AND index_name NOT LIKE '%_PK'))
        LOOP
            DBMS_OUTPUT.PUT_LINE('DROP INDEX ' || res.index_name || ';');
        END LOOP;
END;

/
BEGIN
    COMPARE_SCHEMAS('C##DEV', 'C##PROD');
end;
/
select distinct table_name from all_tab_columns where owner = 'C##DEV';                

