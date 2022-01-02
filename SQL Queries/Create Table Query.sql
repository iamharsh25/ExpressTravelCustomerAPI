Create table Reference (
	ReferenceID		int Not Null,
	RefType			varchar(3) Not Null,
	RefText			varchar(10) Not Null,
	Primary Key (ReferenceID)
);

Create table Contact (
	ContactID		int Not Null,
	SalutationID	int Not Null,
	FirstName		varchar(100) Not Null,
	LastName		varchar(100) Null,
	DOB				Date Null,
	IsStaff			bit Not Null,
	Primary Key (ContactID),
	Foreign Key (SalutationID) References Reference (ReferenceID)
);

Insert Into Reference Values (1, 'SAL', 'Mrs')
Insert Into Reference Values (2, 'SAL', 'Mr')
Insert Into Reference Values (3, 'SAL', 'Miss')
Insert Into Reference Values (4, 'STS', 'Active')
Insert Into Reference Values (5, 'STS', 'InActive')