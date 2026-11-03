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