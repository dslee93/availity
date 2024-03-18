using System;
using System.Formats.Asn1;
using System.Globalization;
using Availity.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace Availity
{
	public class CsvProblem
	{
        public void ExecuteCsv(string inputFilePath, string output, string delimiter = ",")
        {
            //If the directory doesn't exist... creates one to stuff the new csvs
            if (!Directory.Exists(output))
            {
                Console.WriteLine("Directory does not exist.. creating now");
                Directory.CreateDirectory(output);
            }

            //Calls the method to read out the csv and parse it into C# objects
            var customers = ParseCsv<Customer>(inputFilePath, delimiter);

            //Using LINQ expression the customers are grouped by Insurance Company
            var grupedCustomers = customers.GroupBy(customer => customer.InsuranceCompany);

            //Go through groups of customers in insurance company
            foreach(var insurancCompany in grupedCustomers)
            {
                var companyName = insurancCompany.Key;
                var company = insurancCompany.ToList();

                // Filter out the duplicate ids but at the same time sort it so the highest version number is kept.
                var sortedUsers = company.GroupBy(customer => customer.UserId)
                                                .Select(duplicate => duplicate.OrderByDescending(customer => customer.Version).First())
                                                .ToList();

                // After duplicated are sorted out... sort by last and first name (ascending)
                sortedUsers = sortedUsers.OrderBy(record => record.LastName)
                                                 .ThenBy(record => record.FirstName)
                                                 .ToList();
                // Creates the output path
                var outputPath = Path.Combine(output, $"{companyName}.csv");
                //Calls the method to write out the csv files
                WriteCsv<Customer>(outputPath, sortedUsers, delimiter);
            }

        }

        //Goes through csv file and sorts data into C# objects.
        //Uses Generic to allow reusability
        //Delimiter is defaulted to ',' but a different delimiter can be used.
		public List<TEntity> ParseCsv<TEntity>(string csvFilePath, string delimiter = ",")
		{
			var results = new List<TEntity>();
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = delimiter }))
            {
                results = csv.GetRecords<TEntity>().ToList();
            }
            return results;
        }

        //Takes the grouped data by insurance company and turns it into csv files.
        public void WriteCsv<TEntity>(string outputPath, List<TEntity> data, string delimiter = ",")
        {
            using (var writer = new StreamWriter(outputPath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = delimiter }))
            {
                csv.WriteRecords(data);
            }
        }
	}
}

