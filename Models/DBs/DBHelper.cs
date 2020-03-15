﻿using product_and_receipt.Models.DBs.structures;
using product_and_receipt.Models.DBs.Tables;
using System.Collections.Generic;

namespace product_and_receipt.Models.DBs
{
    public class DBHelper : OdbcHelper
    {
        private CompanyTable _CompanyTable { get; set; }
        private ProductTable _ProductTable { get; set; }

        public CompanyTable CompanyTable => _CompanyTable;
        public ProductTable ProductTable => _ProductTable;

        public DBHelper(string dsn, string id, string password, LogFunc log = null) : base(dsn, id, password, log)
        {
            _CompanyTable = new CompanyTable(dsn, id, password, log);
            _ProductTable = new ProductTable(dsn, id, password, log);
        }


        public List<CompanySummaryData> GetCompanySummaries()
        {
            List<CompanySummaryData> list = new List<CompanySummaryData>();

            var companies = _CompanyTable.Get();
            foreach (var company in companies)
            {
                var products = _ProductTable.Get(company.Uid);

                var item = new CompanySummaryData(company, products);

                list.Add(item);
            }

            return list;
        }
    }
}