-- init-user.sql
CREATE USER IF NOT EXISTS 'IvanKobtsev'@'%' IDENTIFIED BY '901321Kobcev';
GRANT ALL PRIVILEGES ON AnchorCalculatorDB.* TO 'IvanKobtsev'@'%';
FLUSH PRIVILEGES;