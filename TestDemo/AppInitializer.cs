using NUnit.Core;
using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TestDemo
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android
                    .ApkFile ("/Users/Moe Sadoon/Desktop/Lab Materials/Binaries/TaskyPro/Android/com.xamarin.samples.taskyandroid.apk") //specify apk file
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }
    }
}

