﻿using Demo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;


namespace Demo.Controllers.Json
{
    public class JsonView
    {
        public List<Company> listroot(List<Company> model, String data)
        {
            model = new List<Company>();
            RootObject root = JsonConvert.DeserializeObject<RootObject>(data);
            model = root.result;
            return model;
        }
        public Company uniroot(Company model, String data)
        {
            Root root = JsonConvert.DeserializeObject<Root>(data);
            model = root.result;
            return model;
        }
        public Login LoginDetails(String data)
        {
            Login loginDetails = JsonConvert.DeserializeObject<Login>(data);
            return loginDetails;
        }
    }
    public class RootObject
    {
        public List<Company> result { get; set; }
    }
    public class Root
    {
        public Company result { get; set; }
    }
    public class HiringJson
    {
        public List<Hiring> listroot(List<Hiring> model, String data)
        {
            model = new List<Hiring>();
            RootObject root = JsonConvert.DeserializeObject<RootObject>(data);
            model = root.result;
            return model;
        }
        public Hiring uniroot(Hiring model, String data)
        {
            Root root = JsonConvert.DeserializeObject<Root>(data);
            model = root.result;
            return model;
        }
        public class RootObject
        {
            public List<Hiring> result { get; set; }
        }
        public class Root
        {
            public Hiring result { get; set; }
        }
    }
    public class Login
    {
        public bool success { get; set; }
        public User user { get; set; }
        public Student student { get; set; }
        public Sessions sessions { get; set; }
    }
}