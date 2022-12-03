SET SERVEROUTPUT ON;
DROP Table MyTable PURGE;
CREATE TABLE MyTable (id NUMBER UNIQUE NOT NULL, value NUMBER NOT NULL);
DECLARE
    counter NUMBER;
BEGIN
    counter:=1;
    LOOP
        INSERT INTO MyTable
        VALUES(counter, dbms_random.RANDOM());
        counter:=counter+1;
        EXIT WHEN counter>10000;
    END LOOP;
END;
/
select * from MyTable;

CREATE OR REPLACE FUNCTION task3
return VARCHAR2
IS
    even NUMBER;
    odd NUMBER;
    result VARCHAR2(20);
BEGIN
    SELECT COUNT(*) INTO even FROM MyTable WHERE REMAINDER(value, 2) = 0;
    SELECT COUNT(*) INTO odd FROM MyTable WHERE REMAINDER(value, 2) = 1;

    IF even > odd THEN
        result:='TRUE';
    ELSIF even < odd THEN
        result:='FALSE';
    ELSE 
        result:='EQUAL';
    END IF;
    
    RETURN RESULT;
END task3;
/
BEGIN
    dbms_output.put_line(task3());
END;
/

CREATE OR REPLACE FUNCTION task4(
ID IN NUMBER)
RETURN VARCHAR2
IS
    result varchar2(100);
BEGIN
    result:= 'INSERT INTO MyTable VALUES(' || TO_CHAR(ID)|| ',' || TO_CHAR(dbms_random.RANDOM()) || ');';
    RETURN result;
END task4;
/
BEGIN
    DBMS_OUTPUT.PUT_LINE(task4(100));
END;
/

CREATE OR REPLACE PROCEDURE MyInsert(id IN NUMBER, value IN number)
IS
BEGIN
    INSERT INTO MyTable VALUES(id, value);
END MyInsert;
/
CREATE OR REPLACE PROCEDURE MyUpdate(searchingId IN NUMBER, newValue IN number)
IS
BEGIN
    UPDATE MyTable SET value = newValue WHERE id=searchingId;
END MyUpdate;
/
CREATE OR REPLACE PROCEDURE MyDelete(deletingId IN NUMBER)
IS
BEGIN
    DELETE FROM MyTable WHERE id=deletingid;
END MyDelete;
/

CREATE OR REPLACE FUNCTION task6(salary IN NUMBER, annualBonus IN NUMBER)
RETURN NUMBER
IS 
    result NUMBER;
BEGIN
    IF annualBonus > 100 OR annualBonus < 0 OR MOD(annualBonus, 1) != 0 THEN
        DBMS_OUTPUT.PUT_LINE('Wrong annualBonus.');
        RETURN result;
    ELSIF salary < 0 THEN
        DBMS_OUTPUT.PUT_LINE('Salary less zero.');
        RETURN result;
    END IF;
    result:=(1+annualBonus*0.01)*12*salary;
    return result;
END task6;
/

BEGIN
    DBMS_OUTPUT.PUT_LINE(task6(100, 2));
END;
/