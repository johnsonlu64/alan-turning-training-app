# SQL Coding Standards

### 1. SQL Naming Convention – Lowercase and snake_case (Your Initials)

**Naming:** // Use lowercase letters and underscores when naming SQL tables and columns. Avoid camelCase and spaces in identifiers.

**Bad:**
```sql
CREATE TABLE StudentRecords (
    StudentID INTEGER,
    FirstName VARCHAR(50)
);
```

**Good:**
```sql
CREATE TABLE student_records (
    student_id INTEGER,
    first_name VARCHAR(50)
);
```

### 2. SQL Keywords – Use Uppercase (Your Initials)

**Capitalization:** // SQL reserved keywords must always be written in uppercase, such as SELECT, FROM, WHERE, INSERT, UPDATE, and DELETE.

**Bad:**
```sql
select first_name
from students
where student_id = 1;
```

**Good:**
```sql
SELECT first_name
FROM students
WHERE student_id = 1;
```

### 3. SQL Indentation – Use Four Spaces (Your Initials)

**Indentation:** // Use four spaces to indent SQL statements when organizing table definitions and longer queries. Keep related statements organized and easy to read.

**Bad:**
```sql
CREATE TABLE students (
student_id INTEGER,
first_name VARCHAR(50),
last_name VARCHAR(50)
);
```

**Good:**
```sql
CREATE TABLE students (
    student_id INTEGER,
    first_name VARCHAR(50),
    last_name VARCHAR(50)
);
```

### 4. SQL Spacing – Use Consistent Spacing (Your Initials)

**Spacing:** // Always include a space before and after comparison operators (=, >, <), and after commas when separating columns or values.

**Bad:**
```sql
SELECT first_name,last_name
FROM students
WHERE student_id=1;
```

**Good:**
```sql
SELECT first_name, last_name
FROM students
WHERE student_id = 1;
```

### 13. SQL Comments - Use Comments to Explain Complex Logic (Your Initials)

**Comments:** // Use comments to explain the purpose of complex queries, non-obvious logic, or business rules. Keep comments concise and up to date.

**Bad:**
```sql
SELECT s.first_name, s.last_name, c.course_name
FROM students s
JOIN enrollments e ON e.student_id = s.student_id
JOIN courses c ON c.course_id = e.course_id
WHERE e.grade > 70;
```

**Good:**
```sql
-- Retrieve students who passed their courses with a grade above 70
SELECT s.first_name, s.last_name, c.course_name
FROM students s
JOIN enrollments e ON e.student_id = s.student_id
JOIN courses c ON c.course_id = e.course_id
WHERE e.grade > 70; -- passing threshold
```

### 14. SQL Data Types - Choose Appropriate Data Types (Your Initials)

**Data Types:** // Use the most appropriate data type for each column based on the data it will store. Avoid using overly large or incorrect types.

**Bad:**
```sql
CREATE TABLE students (
    student_id VARCHAR(100),
    age VARCHAR(10),
    gpa VARCHAR(10)
);
```

**Good:**
```sql
CREATE TABLE students (
    student_id INTEGER,
    age INTEGER,
    gpa DECIMAL(3,2)
);
```

### 15. SQL Primary Keys - Define a Primary Key for Every Table (Your Initials)

**Primary Keys:** // Every table should have a primary key to uniquely identify each row. Prefer an integer primary key.

**Bad:**
```sql
CREATE TABLE students (
    student_id INTEGER,
    first_name VARCHAR(50),
    last_name VARCHAR(50)
);
```

**Good:**
```sql
CREATE TABLE students (
    student_id INTEGER PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50)
);
```

### 16. SQL Table Aliases - Use Short, Meaningful Aliases (Your Initials)

**Aliases:** // Use short, meaningful table aliases in queries with joins to improve readability. Avoid single-letter aliases unless obvious.

**Bad:**
```sql
SELECT a.first_name, b.course_name
FROM students a
JOIN enrollments c ON c.student_id = a.student_id
JOIN courses b ON b.course_id = c.course_id;
```

**Good:**
```sql
SELECT s.first_name, c.course_name
FROM students s
JOIN enrollments e ON e.student_id = s.student_id
JOIN courses c ON c.course_id = e.course_id;
```
