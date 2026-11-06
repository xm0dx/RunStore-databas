-- Active: 1793867752270@@ep-dark-mouse-zak599am.c-2.eu-west-2.aws.neon.tech@5432@skoldb
-- här testar jag frågorna i databasen innan de läggs in i C#-koden

-- alla produkter, kolumnerna kommer i samma ordning som i tabellen
SELECT * FROM products;

SELECT * FROM suppliers;

-- frågan som ProductServices.List() använder
-- suppliername finns i suppliers, därför INNER JOIN
SELECT
    products.id,
    itemnumber,
    productname,
    description,
    imageurl,
    price,
    suppliername
FROM products INNER JOIN suppliers
    ON products.supplier_id = suppliers.id;
