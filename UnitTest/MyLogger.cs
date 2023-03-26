using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

static class MyLogger
{

    public static ILoggerFactory LoggerFactory { get; }

    static MyLogger()
    {
        LoggerFactory = new LoggerFactory();
        //((LoggerFactory)LoggerFactory).AddConsole();
    }
}
