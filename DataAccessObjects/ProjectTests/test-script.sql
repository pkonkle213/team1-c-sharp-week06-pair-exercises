-- Your SQL script to set up your database should go here
--This runs every time our project runs

--Delete all of the data in the correct order
DELETE FROM project_employee
DELETE FROM employee
DELETE FROM project
DELETE FROM department

--Insert fake info
SET IDENTITY_INSERT department ON
INSERT INTO department (department_id,name)
VALUES(1,'.NET Supreme');
SET IDENTITY_INSERT department OFF

SET IDENTITY_INSERT employee ON
INSERT INTO employee (employee_id,department_id,first_name,last_name,job_title,birth_date,hire_date)
VALUES (1,1,'William','Randolph','MasterYoda','1984/10/17','2021/9/13'),
		(2,1,'Phillip','Konkle','PadawanLuke','1987/08/20','2021/9/13');
SET IDENTITY_INSERT employee OFF

SET IDENTITY_INSERT project ON
INSERT INTO project (project_id,name,from_date,to_date)
VALUES (1,'TechElevator','2021/10/17','2021/10/22');
SET IDENTITY_INSERT project OFF

INSERT INTO project_employee (project_id,employee_id)
VALUES (1,1);