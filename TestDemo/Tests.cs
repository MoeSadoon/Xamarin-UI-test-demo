using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TestDemo
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)] this is commented out since we're not using iOS
    public class Tests
    {
        IApp app;
        Platform platform;

        readonly Func<AppQuery, AppQuery> AddButton;
        readonly Func<AppQuery, AppQuery> NameField;
        readonly Func<AppQuery, AppQuery> DescriptionField;

        public Tests(Platform platform)
        {
            this.platform = platform;


            if (platform == Platform.iOS)
            {
                AddButton = c => c.Button("Add");
                NameField = c => c.Class("UITextField").Index(0);
                DescriptionField = c => c.Class("UITextField").Index(1);
            }
            else
            {
                AddButton = c => c.Marked("Add Task");
                NameField = c => c.Class("EditText").Index(0);
                DescriptionField = c => c.Class("EditText").Index(1);            
            }
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Ignore]
        [Test]
        public void AppLaunches()
        {
            app.Repl();            
        }

        [Test]
        public void TaskyPro_CreatingATask_ShouldBeSuccessful()
        {
            AddNewTaskMethod("Get Milk", "Pick up some milk");
            app.WaitForElement(c => c.Marked("Get Milk"));
        }

        [Test]
        public void TaskyPro_DeletingATask_ShouldBeSuccessful()
        {
            AddNewTaskMethod("Test Delete", "This should be deleted");
            app.WaitForElement("Test Delete");
            DeleteTaskMethod("Test Delete");
            app.WaitForNoElement("Test Delete");
            Assert.AreEqual(0, app.Query("Test Delete").Length);
        }

        void AddNewTaskMethod(string name, string description)
        {
            if (platform == Platform.iOS)
            {
                app.Tap(c => c.Class("Add"));
                app.EnterText(c => c.Class("UITextField").Index(0), name);
                app.EnterText(c => c.Class("UITextField").Index(1), description);
            }
            else
            {
                app.Tap("Add Task");
                app.EnterText(c => c.Class("EditText").Index(0), name);
                app.EnterText(c => c.Class("EditText").Index(1), description);
            }
            app.Tap("Save");
        }

        void DeleteTaskMethod(string name)
        {
        
            if (platform == Platform.iOS)
            {
                //
            }
            else
            {                
                app.Tap(name); ;
                app.Tap("Delete"); 
            }
        }
    }
}

