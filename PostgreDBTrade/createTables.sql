CREATE TABLE Suppliers
(
	SupplierID SERIAL PRIMARY KEY,
	CompanyName TEXT,
	ContactName TEXT,
	ContactTitle TEXT,
	Address TEXT,
	City TEXT,
	Region TEXT,
	PostalCode TEXT,
	Country TEXT,
	Phone TEXT,
	Fax TEXT,
	HomePage TEXT
);

CREATE TABLE Regions
(
	RegionID SERIAL PRIMARY KEY,
	RegionDescription TEXT
);

CREATE TABLE Shippers
(
	ShipperID SERIAL PRIMARY KEY,
	CompanyName TEXT,
	Phone TEXT
);

CREATE TABLE CustomerDemographics
(
	CustomerTypeID SERIAL PRIMARY KEY,
	CustomerDect TEXT
);

CREATE TABLE Categories
(
	CategoryID SERIAL PRIMARY KEY,
	CategoryName TEXT,
	Description TEXT,
	Picture BYTEA
);

CREATE TABLE Customers
(
	CustomerID SERIAL PRIMARY KEY,
	CompanyName TEXT,
	ContactName TEXT,
	ContactTitle TEXT,
	Adress TEXT,
	City TEXT,
	Region TEXT,
	PostalCode TEXT,
	Country TEXT,
	Phone TEXT,
	Fax TEXT
);

CREATE TABLE CustomerCustomerDemo
(
	CustomerID INTEGER,
	CustomerTypeID INTEGER,
	PRIMARY KEY(CustomerID, CustomerTypeID),
	CONSTRAINT FK_Customer FOREIGN KEY(CustomerID) REFERENCES Customers(CustomerID),
	CONSTRAINT FK_CustomerType FOREIGN KEY(CustomerTypeID) REFERENCES CustomerDemographics(CustomerTypeID)
);

CREATE TABLE Territories
(
	TerritoryID SERIAL PRIMARY KEY,
	TerritoryDescription TEXT,
	RegionID INTEGER NOT NULL,
	CONSTRAINT FK_Region FOREIGN KEY(RegionID) REFERENCES Regions(RegionID)
);

CREATE TABLE Employees
(
	EmployeeID SERIAL PRIMARY KEY,
	LastName TEXT,
	FirstName TEXT,
	Title TEXT,
	TitleOfCourtesy TEXT,
	BirthDate DATE,
	HireDate DATE,
	Address TEXT,
	City TEXT,
	Region TEXT,
	PostalCode TEXT,
	Country TEXT,
	HomePhone TEXT,
	Extension TEXT,
	Photo BYTEA,
	Notes TEXT,
	ReportsTo TEXT,
	PhotoPath TEXT
);

CREATE TABLE EmployeeTerritories
(
	EmployeeID SERIAL,
	TerritoryID SERIAL,
	PRIMARY KEY(EmployeeID, TerritoryID),
	CONSTRAINT FK_Employee FOREIGN KEY(EmployeeID) REFERENCES Employees(EmployeeID),
	CONSTRAINT FK_Territory FOREIGN KEY(TerritoryID) REFERENCES Territories(TerritoryID)
);

CREATE TABLE Products
(
	ProductID SERIAL PRIMARY KEY,
	ProductName TEXT,
	SupplierID INTEGER NOT NULL,
	CategoryID INTEGER NOT NULL,
	QuantityPerUnit INTEGER,
	UnitPrice NUMERIC(10,2),
	UnitsInStock INTEGER,
	UnitsOnOrder INTEGER,
	ReorderLevel INTEGER,
	Discontinued BOOLEAN,
	CONSTRAINT FK_Supplier FOREIGN KEY(SupplierID) REFERENCES Suppliers(SupplierID),
	CONSTRAINT FK_Category FOREIGN KEY(CategoryID) REFERENCES Categories(CategoryID)
);

CREATE TABLE Orders
(
	OrderID SERIAL PRIMARY KEY,
	CustomerID INTEGER NOT NULL,
	EmployeeID INTEGER NOT NULL,
	OrderDate DATE,
	RequiredDate DATE,
	ShippedDate DATE,
	ShipVia TEXT,
	Freight TEXT,
	ShipName TEXT,
	ShipAddress TEXT,
	ShipCity TEXT,
	ShipRegion TEXT,
	ShipPostalCode TEXT,
	ShipCountry TEXT,
	CONSTRAINT FK_Customer FOREIGN KEY(CustomerID) REFERENCES Customers(CustomerID),
	CONSTRAINT FK_Employee FOREIGN KEY(EmployeeID) REFERENCES Employees(EmployeeID)
);

CREATE TABLE OrderDetails
(
	OrderID INTEGER,
	ProductID INTEGER,
	UnitPrice NUMERIC(10,2),
	Quantity INTEGER,
	Discount NUMERIC(4,2),
	PRIMARY KEY(OrderID, ProductID),
	CONSTRAINT FK_Order FOREIGN KEY(OrderID) REFERENCES Orders(OrderID),
	CONSTRAINT FK_Product FOREIGN KEY(ProductID) REFERENCES Products(ProductID)
);