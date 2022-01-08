using System;
using System.Collections.Generic;
using Ensek.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;

namespace Ensek.Services
{
    public class MeterReadingService
    {
        private static readonly Dictionary<long, string> TestAccountDetails = new Dictionary<long, string>
        {
            { 2344, "Tommy"},
            { 2233, "Barry"},
            { 8766, "Sally"},
            { 2345, "Jerry"},
            { 2346, "Ollie"},
            { 2347, "Tara"},
            { 2348, "Tammy"},
            { 2349, "Simon"},
            { 2350, "Colin"},
            { 2351, "Gladys"},
            { 2352, "Greg"},
            { 2353, "Tony"},
            { 2355, "Arthur"},
            { 2356, "Craig"},
            { 6776, "Laura"},
            { 4534, "JOSH"},
            { 1234, "Freya"},
            { 1239, "Noddy"},
            { 1240, "Archie"},
            { 1241, "Lara"},
            { 1242, "Tim"},
            { 1243, "Graham"},
            { 1244, "Tony"},
            { 1245, "Neville"},
            { 1246, "Jo"},
            { 1247, "Jim"},
            { 1248, "Pam"}
         };

        // Helper function to ensure that meter reading is associated with
        // a valid user in the database
        public Boolean CheckValidUser(long accountID)
        {
            if (TestAccountDetails.ContainsKey(accountID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public Boolean LoadForm(IFormFile formFile)
        //{
            
        //}
    }
}
