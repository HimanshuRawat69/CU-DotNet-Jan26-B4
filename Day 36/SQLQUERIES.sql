--1
SELECT p.ProductName , c.CategoryName
from Products p
INNER JOIN Categories c on p.CategoryID = c.CategoryID;
--2
SELECT o.OrderID, c.CompanyName
FROM Orders o
INNER JOIN Customers c ON o.CustomerID = c.CustomerID;
--3
SELECT p.ProductName, s.CompanyName
FROM Products p
INNER JOIN Suppliers s ON p.SupplierID = s.SupplierID;
--4
SELECT o.OrderID, o.OrderDate, e.FirstName, e.LastName
FROM Orders o
INNER JOIN Employees e ON o.EmployeeID = e.EmployeeID;
--5
SELECT o.OrderID, s.CompanyName
FROM Orders o
INNER JOIN Shippers s ON o.ShipVia = s.ShipperID
WHERE o.ShipCountry = 'France';
--6
SELECT c.CategoryName, SUM(p.UnitsInStock) AS TotalUnitsInStock
FROM Products p
INNER JOIN Categories c ON p.CategoryID = c.CategoryID
GROUP BY c.CategoryName;
--7
SELECT c.CompanyName, SUM(od.UnitPrice * od.Quantity) AS TotalSpent
FROM Customers c
INNER JOIN Orders o ON c.CustomerID = o.CustomerID
INNER JOIN [Order Details] od ON o.OrderID = od.OrderID
GROUP BY c.CompanyName;
--8
SELECT e.LastName, COUNT(o.OrderID) AS TotalOrders
FROM Employees e
INNER JOIN Orders o ON e.EmployeeID = o.EmployeeID
GROUP BY e.LastName;
--9
SELECT s.CompanyName, SUM(o.Freight) AS TotalFreight
FROM Orders o
INNER JOIN Shippers s ON o.ShipVia = s.ShipperID
GROUP BY s.CompanyName;
--10
SELECT TOP 5 p.ProductName,
       SUM(od.Quantity) AS TotalQuantitySold
FROM [Order Details] od
INNER JOIN Products p ON od.ProductID = p.ProductID
GROUP BY p.ProductName
ORDER BY TotalQuantitySold DESC;
--11
SELECT ProductName
FROM Products
WHERE UnitPrice > (SELECT AVG(UnitPrice) FROM Products);
--12
SELECT (e.FirstName + ' ' + e.LastName) AS Employee, (m.FirstName + ' ' + m.LastName) AS Manager
FROM Employees e
LEFT JOIN Employees m ON e.ReportsTo = m.EmployeeID;
--13
SELECT CompanyName
FROM Customers c
WHERE NOT EXISTS (
    SELECT *
    FROM Orders o
    WHERE o.CustomerID = c.CustomerID
);
--14
SELECT OrderID
FROM [Order Details]
GROUP BY OrderID
HAVING SUM(UnitPrice * Quantity) >
(
    SELECT AVG(OrderTotal) FROM (
        SELECT SUM(UnitPrice * Quantity) AS OrderTotal
        FROM [Order Details]
        GROUP BY OrderID
    ) AS OrderTotals
);
--15
SELECT ProductName
FROM Products p
WHERE NOT EXISTS (
    SELECT *
    FROM Orders o
    INNER JOIN [Order Details] od ON o.OrderID = od.OrderID
    WHERE od.ProductID = p.ProductID
      AND YEAR(o.OrderDate) > 1997
);
--16
SELECT (e.FirstName + ' ' + e.LastName) AS Employee,
       r.RegionDescription
FROM Employees e
INNER JOIN EmployeeTerritories et ON e.EmployeeID = et.EmployeeID
INNER JOIN Territories t ON et.TerritoryID = t.TerritoryID
INNER JOIN Region r ON t.RegionID = r.RegionID;
--17
SELECT c.CompanyName AS Customer,
       s.CompanyName AS Supplier,
       c.City
FROM Customers c
INNER JOIN Suppliers s ON c.City = s.City;
--18
SELECT c.CompanyName
FROM Customers c
INNER JOIN Orders o ON c.CustomerID = o.CustomerID
INNER JOIN [Order Details] od ON o.OrderID = od.OrderID
INNER JOIN Products p ON od.ProductID = p.ProductID
GROUP BY c.CompanyName
HAVING COUNT(DISTINCT p.CategoryID) > 3;
--19
SELECT SUM(od.UnitPrice * od.Quantity) AS TotalRevenue
FROM [Order Details] od
INNER JOIN Products p ON od.ProductID = p.ProductID
WHERE p.Discontinued = 'True';
--20
SELECT c.CategoryName, p.ProductName, p.UnitPrice
FROM Products p
INNER JOIN Categories c ON p.CategoryID = c.CategoryID
WHERE p.UnitPrice =
(
    SELECT MAX(p2.UnitPrice)
    FROM Products p2
    WHERE p2.CategoryID = p.CategoryID
);