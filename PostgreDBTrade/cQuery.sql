SELECT Orders.OrderID,
Orders.OrderDate,
Customers.CompanyName,
Products.ProductName,
Products.UnitPrice,
OrderDetails.Quantity,
SUM(OrderDetails.UnitPrice) AS Order_Detail_Value
FROM Orders
INNER JOIN Customers ON Customers.CustomerID = Orders.CustomerID
INNER JOIN OrderDetails ON OrderDetails.OrderID = Orders.OrderID
INNER JOIN Products ON Products.ProductID = OrderDetails.ProductID
WHERE Orders.OrderID = 10255
GROUP BY Orders.OrderID,
Customers.CompanyName,
Products.ProductName,
Products.UnitPrice,
OrderDetails.Quantity;