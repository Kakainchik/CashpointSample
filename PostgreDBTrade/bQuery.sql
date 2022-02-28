SELECT Orders.OrderID,
OrderDate,
CompanyName,
ProductID,
UnitPrice,
Quantity
FROM Orders, Customers, OrderDetails
WHERE Orders.OrderID = 10255 AND Orders.OrderID = OrderDetails.OrderID;