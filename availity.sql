a. Write a SQL query that will produce a reverse-sorted list (alphabetically by name) of customers (first and last
names) whose last name begins with the letter ‘S.’

SELECT *
FROM Customer c
WHERE c.LastName LIKE 'S%'
ORDER BY c.LastName DESC, c.FirstName DESC;

b. Write a SQL query that will show the total value of all orders each customer has placed in the past six
months. Any customer without any orders should show a $0 value.


SELECT c.FirstName, c.LastName, COALESCE(SUM(ol.Cost * ol.Quantity), 0) AS TotalOrder
FROM Customer c
LEFT JOIN Order o 
ON c.CustID = o.CustomerID
LEFT JOIN OrderLine ol 
ON o.OrderID = ol.OrdID
WHERE o.OrderDate >= DATEADD(MONTH, -6, GETDATE())
ORDER BY c.LastName DESC, c.FirstName DESC;


c. Amend the query from the previous question to only show those customers who have a total order value of
more than $100 and less than $500 in the past six months.

SELECT c.FirstName, c.LastName, SUM(ol.Cost * ol.Quantity) AS TotalOrder t
FROM Customer c
LEFT JOIN Order o 
ON c.CustID = o.CustomerID
LEFT JOIN OrderLine ol 
ON o.OrderID = ol.OrdID
WHERE o.OrderDate >= DATEADD(MONTH, -6, GETDATE())
ORDER BY c.LastName DESC, c.FirstName DESC;
HAVING t BETWEEN 100 AND 500;
