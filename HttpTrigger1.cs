using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;

namespace Company.Function
{
    public static class HttpTrigger1
    {
        private static Lazy<MongoClient> lazyClient = new Lazy<MongoClient>(InitializeMongoClient);
        private static MongoClient mongoClient => lazyClient.Value;

        private static MongoClient InitializeMongoClient()
        {
            var uri = "<ADD YOUR CONNECTION STRING HERE>";
            var client = new MongoClient(uri);

            return client;
        }

        [Function("HttpTrigger1")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var dbs = string.Join(",", mongoClient.ListDatabaseNames().ToList());

            var response = req.CreateResponse(HttpStatusCode.OK);

            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString(dbs);

            return response;
        }
    }
}
