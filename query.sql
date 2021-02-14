CREATE TABLE IF NOT EXISTS Customers (
	Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	Name TEXT NOT NULL,
	Created DATETIME NOT NULL
); 


CREATE TABLE IF NOT EXISTS Issues (
	Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	CustomerId INTEGER NOT NULL,
	Title TEXT NOT NULL,
	Description TEXT NOT NULL,
	Status TEXT NOT NULL,
	Created DATETIME NOT NULL,
	Category TEXT NOT NULL,
	PictureSource TEXT,
	FOREIGN KEY (CustomerId) REFERENCES Customers(Id)
); 


CREATE TABLE IF NOT EXISTS Comments (
	Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	IssueId INTEGER NOT NULL,
	Description TEXT NOT NULL,
	Created DATETIME NOT NULL,
	FOREIGN KEY (IssueId) REFERENCES Issues(Id)
);

--CREATE TABLE IF NOT EXISTS IssuePicture (
--	Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
--	IssueId INTEGER NOT NULL,
--	PictureSource TEXT,
--	Created DATETIME NOT NULL,
--	FOREIGN KEY (IssueId) REFERENCES Issues(Id)
--)


-----------------------------------------------------------

INSERT INTO Customers VALUES(null, @Name, @Created);
INSERT INTO Issues VALUES(null, @CustomerId, @Title, @Description, @Status, @Created);
INSERT INTO Comments VALUES(null, IssueId, @Description, @Created);

SELECT last_insert_rowid()
eller
SELECT Id FROM Customers WHERE Name = @Name;




-----------------------------------------------------------


