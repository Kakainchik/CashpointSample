SELECT Suppliers.CompanyName,
COUNT(DISTINCT Orders.OrderID) AS Number_Of_Unique_Orders
FROM Suppliers
JOIN Products ON Products.SupplierID = Suppliers.SupplierID
JOIN OrderDetails ON OrderDetails.ProductID = Products.ProductID
JOIN Orders ON OrderDetails.OrderID = Orders.OrderID
GROUP BY Suppliers.CompanyName;