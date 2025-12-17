USE LearnSphereDB;

CREATE TABLE Administrator (
	AdministratorID INT PRIMARY KEY,
	Username NVARCHAR(50) UNIQUE NOT NULL,
	Email NVARCHAR(50) UNIQUE NOT NULL,
	Password NVARCHAR(50) NOT NULL,

	CONSTRAINT CHK_AdministratorID CHECK (AdministratorID >= 0)
)

CREATE TABLE Learner (
	LearnerID INT PRIMARY KEY,
	Username NVARCHAR(50) UNIQUE NOT NULL,
	Email NVARCHAR(50) UNIQUE NOT NULL,
	Password NVARCHAR(50) NOT NULL,

	CONSTRAINT CHK_LearnerID CHECK (LearnerID >= 0)
)

CREATE TABLE Educator (
	EducatorID INT PRIMARY KEY,
	Username NVARCHAR(50) UNIQUE NOT NULL,
	Email NVARCHAR(50) UNIQUE NOT NULL,
	Password NVARCHAR(50) NOT NULL,

	CONSTRAINT CHK_EducatorID CHECK (EducatorID >= 0)
)

CREATE TABLE Domain (
	DomainName NVARCHAR(50) PRIMARY KEY,
	Description NVARCHAR(50)
)

CREATE TABLE DigitalResource (
	DigitalResourceID INT PRIMARY KEY,
	Title NVARCHAR(50) UNIQUE NOT NULL,
	Author NVARCHAR(50),
	PublicationYear INT,
	DomainName NVARCHAR(50) NOT NULL,
	Category NVARCHAR(50) NOT NULL,
	Locator NVARCHAR(500) NOT NULL,

	CONSTRAINT FK_DomainName_DigitalResource FOREIGN KEY (DomainName) REFERENCES Domain(DomainName),
	CONSTRAINT CHK_DigitalResourceID CHECK (DigitalResourceID >= 0),
	CONSTRAINT CHK_PublicationYear CHECK (PublicationYear IS NULL OR PublicationYear BETWEEN 1900 AND 2050),
	CONSTRAINT CHK_Category CHECK (Category IN ('Book', 'Article', 'Lecture', 'Other'))
)

CREATE TABLE Forum (
	ForumID INT PRIMARY KEY,
	Topic NVARCHAR(50) NOT NULL,
	DomainName NVARCHAR(50) NOT NULL,
	Tag1 NVARCHAR(50),
	Tag2 NVARCHAR(50)

	CONSTRAINT FK_DomainName_Forum FOREIGN KEY (DomainName) REFERENCES Domain(DomainName),
	CONSTRAINT CHK_ForumID CHECK (ForumID >= 0)

)

CREATE TABLE ExamPaper (
	ExamPaperID INT PRIMARY KEY,
	Title NVARCHAR(50) NOT NULL,
	EducatorID INT NOT NULL,
	DomainName NVARCHAR(50) NOT NULL,
	Tag1 NVARCHAR(50),
	Tag2 NVARCHAR(50),

	CONSTRAINT FK_EducatorID_ExamPaper FOREIGN KEY (EducatorID) REFERENCES Educator(EducatorID),
	CONSTRAINT FK_DomainName_ExamPaper FOREIGN KEY (DomainName) REFERENCES Domain(DomainName),
	CONSTRAINT CHK_ExamPaperID CHECK (ExamPaperID >= 0)
)

CREATE TABLE Assessment (
		ExamPaperID INT NOT NULL,
		LearnerID INT NOT NULL,
		Marks INT,

		CONSTRAINT PK_Assessment PRIMARY KEY (ExamPaperID, LearnerID),
		CONSTRAINT FK_ExamPaperID_Assessment FOREIGN KEY (ExamPaperID) REFERENCES ExamPaper(ExamPaperID),
		CONSTRAINT FK_LearnerID_Assessment FOREIGN KEY (LearnerID) REFERENCES Learner(LearnerID),
		CONSTRAINT CHK_Marks CHECK (Marks IS NULL OR Marks BETWEEN 0 AND 100)
)