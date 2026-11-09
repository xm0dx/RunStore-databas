-- min skoldb hade kolumnen customerid, läraren använder customer_id
ALTER TABLE salesorders RENAME COLUMN customerid TO customer_id;
