using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnSphere.DAL
{
    public class Table
    {
        public static class Administrator
        {
            public const string name = "Administrator";
            public const string AdministratorID = "AdministratorID";
            public const string Username = "Username";
            public const string Email = "Email";
            public const string Password = "Password";
        }

        public static class Learner
        {
            public const string name = "Learner";
            public const string LearnerID = "Learner ID";
            public const string Username = "Username";
            public const string Email = "Email";
            public const string Password = "Password";
        }

        public static class Educator
        {
            public const string name = "Educator";
            public const string EducatorID = "EducatorID";
            public const string Username = "Username";
            public const string Email = "Email";
            public const string Password = "Password";
        }

        public static class Domain
        {
            public const string name = "Domain";
            public const string DomainName = "DomainName";
            public const string Description = "Description";
        }

        public static class DigitalResource
        {
            public const string name = "DigitalResource";
            public const string DigitalResourceID = "DigitalResourceID";
            public const string Title = "Title";
            public const string Author = "Author";
            public const string PublicationYear = "PublicationYear";
            public const string DomainName = "DomainName";
            public const string Category = "Category";
            public const string Locator = "Locator";
        }

        public static class Forum
        {
            public const string name = "Forum";
            public const string ForumID = "ForumID";
            public const string Topic = "Topic";
            public const string DomainName = "DomainName";
            public const string Tag1 = "Tag1";
            public const string Tag2 = "Tag2";
        }

        public static class LearnerPost
        {
            public const string name = "LearnerPost";
            public const string LearnerPostID = "LearnerPostID";
            public const string ForumID = "ForumID";
            public const string LearnerID = "LearnerID";
            public const string Content = "Content";
            public const string CreatedAt = "CreatedAt";
        }

        public static class EducatorPost
        {
            public const string name = "EducatorPost";
            public const string EducatorPostID = "EducatorPostID";
            public const string ForumID = "ForumID";
            public const string EducatorID = "EducatorID";
            public const string Content = "Content";
            public const string CreatedAt = "CreatedAt";
        }

        public static class ExamPaper
        {
            public const string name = "ExamPaper";
            public const string ExamPaperID = "ExamPaperID";
            public const string Title = "Title";
            public const string EducatorID = "EducatorID";
            public const string DomainName = "DomainName";
            public const string Tag1 = "Tag1";
            public const string Tag2 = "Tag2";
        }

        public static class Assessment
        {
            public const string name = "Assessment";
            public const string ExamPaperID = "ExamPaperID";
            public const string LearnerID = "LearnerID";
            public const string Marks = "Marks";
        }
    }
}