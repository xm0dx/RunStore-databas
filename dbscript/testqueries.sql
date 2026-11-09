SELECT
    products.id,
    itemnumber,
    productname,
    price,
    suppliername
FROM products INNER JOIN suppliers
    ON products.supplier_id = suppliers.id;

SELECT
    id,
    firstname,
    lastname,
    email,
    COALESCE(phone,'Saknas') AS phone
FROM customers;

SELECT
    salesorders.id,
    ordernumber,
    orderdate,
    firstname,
    lastname,
    email,
    SUM(quantity * price) AS ordervalue
FROM salesorders INNER JOIN customers
    ON salesorders.customer_id = customers.id INNER JOIN orderitems
    ON salesorders.id = orderitems.order_id
GROUP BY salesorders.id,ordernumber,orderdate,firstname,lastname,email;

SELECT
    id,
    suppliername
FROM suppliers WHERE LOWER(suppliername) = 'asics';

SELECT 
    products.id,
    itemnumber,
    productname,
    COALESCE(description,'') AS description,
    COALESCE(imageurl,'') as imageurl,
    price,
    suppliers.id AS supplierId,
    suppliers.suppliername 
FROM products INNER JOIN suppliers
    ON products.supplier_id = suppliers.id 
WHERE itemnumber = '1000';

UPDATE products SET price = 100 WHERE itemnumber = '1000';

SELECT
    id,
    firstname,
    lastname,
    email,
    COALESCE(phone,'Saknas') AS phone
FROM customers
    WHERE email = 'michael@mail.com';