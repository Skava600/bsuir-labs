--Create tables
/*create table STUDENTS
(
    id       number,
    name     varchar2(100),
    group_id number
);

create table GROUPS
(
    id    number,
    name  varchar2(100),
    c_val number
);*/

--Create triggers for groups
//create sequence groups_seq minvalue 1 maxvalue 10000 start with 1 increment by 1;

create or replace trigger group_before_insert
    before insert
    on GROUPS
    for each row
declare
    check_id number;
begin
    select id into check_id from GROUPS where name = :new.name;
    raise_application_error(-20001, 'This name also used');
exception
    when no_data_found then
        :new.id := groups_seq.nextval;
        :new.c_val := 0;
        DBMS_OUTPUT.PUT_LINE('Group created');
end;

create or replace trigger group_before_update
    before update
    on GROUPS
    for each row
declare
    check_id number;
begin
    select id into check_id from GROUPS where name = :new.name;
    raise_application_error(-20001, 'Not unique');
exception
    when no_data_found then
        DBMS_OUTPUT.PUT_LINE('ok');
end;

--create sequence students_seq minvalue 1 maxvalue 10000 start with 1 increment by 1;

create or replace trigger student_before_insert
    before insert
    on STUDENTS
    for each row
declare
    check_group_name varchar2(100);
begin
    select name into check_group_name from GROUPS where id = :new.group_id;
    :new.id := students_seq.nextval;
    DBMS_OUTPUT.PUT_LINE('Student created');
exception
    when no_data_found then
        raise_application_error(-20001, 'Primary key exception');
end;

create or replace trigger student_before_update
    before update
    on STUDENTS
    for each row
declare
    check_group_name varchar2(100);
begin
    select name into check_group_name from GROUPS where id = :new.group_id;
exception
    when no_data_found then
        raise_application_error(-20001, 'Primary key exception');
end;

--Cascade delete group trigger
create or replace trigger group_cascade_delete
    after delete
    on GROUPS
    for each row
begin
    delete from STUDENTS s where s.group_id = :OLD.id;
end;

--Students journal
create table STUDENTS_JOURNAL
(
    op_timestamp     timestamp,
    operation        varchar(10),
    student_id       number,
    student_name     varchar2(100),
    student_group_id number
);

create or replace trigger students_insert_log
    after insert
    on STUDENTS
    for each row
begin
    insert into STUDENTS_JOURNAL (op_timestamp, operation, student_id, student_name, student_group_id)
    VALUES (current_timestamp, 'insert', :new.id, :new.name, :new.group_id);
end;

create or replace trigger students_delete_log
    after delete
    on STUDENTS
    for each row
begin
    insert into STUDENTS_JOURNAL (op_timestamp, operation, student_id, student_name, student_group_id)
    VALUES (current_timestamp, 'delete', :old.id, null, null);
end;

create or replace trigger students_update_log
    after update
    on STUDENTS
    for each row
declare
    log_name     varchar2(100);
    log_group_id number;
begin
    if :new.name IS NULL then
        log_name := :old.name;
    else
        log_name := :new.name;
    end if;

    if :new.group_id IS NULL then
        log_group_id := :old.group_id;
    else
        log_group_id := :new.group_id;
    end if;

    insert into STUDENTS_JOURNAL (op_timestamp, operation, student_id, student_name, student_group_id)
    VALUES (current_timestamp, 'update', :old.id, log_name, log_group_id);
end;

--journal procedure
create or replace procedure journal_recovery(ts timestamp, seconds number) is
    ts_limit timestamp;
begin
    if seconds is null then
        ts_limit := ts;
    else
        ts_limit := current_timestamp - numToDSInterval(seconds, 'second');
    end if;

    delete from STUDENTS;

    for o in (select operation, student_id, student_name, student_group_id
              from STUDENTS_JOURNAL
              where op_timestamp <= ts_limit)
        loop
            if o.operation = 'insert' then
                insert into STUDENTS (id, name, group_id) values (o.student_id ,o.student_name, o.student_group_id);
            elsif o.operation = 'update' then
                update STUDENTS set name = o.student_name, group_id=o.student_group_id where id = o.student_id;
            else
                delete from STUDENTS where id = o.student_id;
            end if;
        end loop;
end;

--students c_val triggers
create or replace trigger students_group_after_insert
    after insert
    on STUDENTS
    for each row
declare
    gId        number;
    groupCount number;
begin
    gId := :new.group_id;

    select c_val into groupCount from GROUPS where id = gid;
    update GROUPS set c_val = groupCount + 1 where id = gId;
end;

create or replace trigger students_group_after_delete
    after delete
    on STUDENTS
    for each row
declare
    gId        number;
    groupCount number;
begin
    gId := :old.group_id;

    select c_val into groupCount from GROUPS where id = gid;
    update GROUPS set c_val = groupCount - 1 where id = gId;
end;

create or replace trigger students_group_after_update
    after update
    on STUDENTS
    for each row
declare
    oldGId     number;
    newGId     number;
    groupCount number;
begin
    oldGId := :old.group_id;
    newGId := :new.group_id;

    select c_val into groupCount from GROUPS where id = oldGId;
    update GROUPS set c_val = groupCount - 1 where id = oldGId;

    select c_val into groupCount from GROUPS where id = newGId;
    update GROUPS set c_val = groupCount + 1 where id = newGId;
end;