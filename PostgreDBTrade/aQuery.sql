SELECT OrderID,
OrderDate,
CompanyName,
ContactName
FROM Orders, Customers
WHERE OrderID = 10255;