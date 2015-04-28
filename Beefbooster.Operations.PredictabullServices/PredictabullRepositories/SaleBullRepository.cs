using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Beefbooster.Operations.PredictabullServices.Models;

namespace Beefbooster.Operations.PredictabullServices.PredictabullRepositories
{
    public enum AvailabilityScope
    {
        All,
        AvailableOnly
    }

    public enum SaleStatusScope
    {
        All,
        Classed
    }

    internal class Constants
    {
        public const Int32 InitializeInt = -1;
        public static readonly short InitializeShort = -1;
        public static readonly byte InitializeByte = 0;
        public static readonly decimal InitializeDecimal = 0;
        public static readonly double InitializeDouble = double.NaN;
        public static readonly float InitializeFloat = float.NaN;
        public static readonly string InitializeString = String.Empty;
        public static readonly DateTime InitializeDateTime = DateTime.MinValue;
        public static readonly int MaxSmallXmlSize = 2048;
    }

    public class SaleBullRepository : BaseRepository, ISaleBullRrepository
    {
        public IEnumerable<SaleBull> Get(string strain, short saleYear, AvailabilityScope scope, SaleStatusScope saleStatus)
        {
            SqlCommand command = BuildCommand("[pb].[GetBulls]");
            AddParameters(command, strain, saleYear, scope, saleStatus);
            OpenConnection(command.Connection);
            SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return ReadData(dataReader);
        }

        private IEnumerable<SaleBull> ReadData(SqlDataReader rdr)
        {
            var lst = new List<SaleBull>();

            int ordCalfSN = rdr.GetOrdinal("Calf_SN");
            int ordStrainCode = rdr.GetOrdinal("Strain_Code");
            int ordHerdCode = rdr.GetOrdinal("Herd_Code");
            int ordYrCode = rdr.GetOrdinal("Yr_Code");
            int ordTagNum = rdr.GetOrdinal("Tag_Num");
            int ordCalfId = rdr.GetOrdinal("Calf_Id");
            int ordBirthDate = rdr.GetOrdinal("Birth_Date");
            int ordCalfRawBirthWt = rdr.GetOrdinal("RawBirth_Wt");
            int ordFeedlotId = rdr.GetOrdinal("Feedlot_Id");
            int ordPen = rdr.GetOrdinal("Pen");
            int ordHideColourCode = rdr.GetOrdinal("HideColour_Code");
            int ordSireCalfId = rdr.GetOrdinal("SireCalf_Id");

            int ordDamId = rdr.GetOrdinal("Dam_Id");
            int ordAgeOfDam = rdr.GetOrdinal("AgeOfDam");
            int ordDamWt = rdr.GetOrdinal("Dam_Wt");
            int ordTeat = rdr.GetOrdinal("Teat");
            int ordUdder = rdr.GetOrdinal("Udder");
            int ordFf = rdr.GetOrdinal("FF");
            int ordFl = rdr.GetOrdinal("FL");
            int ordHf = rdr.GetOrdinal("HF");
            int ordHl = rdr.GetOrdinal("HL");
            int ordMorph = rdr.GetOrdinal("Morph");
            int ordMotil = rdr.GetOrdinal("Motil");
            int ordConc = rdr.GetOrdinal("Conc");
            int ordDisp = rdr.GetOrdinal("Disp");

            int ordBWAdj = rdr.GetOrdinal("BW_ADJ");
            int ordWwAdj = rdr.GetOrdinal("WW_ADJ");
            int ordYwAdj = rdr.GetOrdinal("YW_ADJ");
            int ordH18MwAdj = rdr.GetOrdinal("H18MW_ADJ");
            int ordADGBWAdj = rdr.GetOrdinal("ADG_BW_ADJ");
            int ordBackfatAdj = rdr.GetOrdinal("BACKFAT_ADJ");
            int ordScrotcircAdj = rdr.GetOrdinal("SCROTCIRC_ADJ");

            int ordBWEbv = rdr.GetOrdinal("BW_EBV");
            int ordWwdEbv = rdr.GetOrdinal("WWD_EBV");
            int ordYwtEbv = rdr.GetOrdinal("YWT_EBV");
            int ordScEbv = rdr.GetOrdinal("SC_EBV");
            int ordBfEbv = rdr.GetOrdinal("BF_EBV");
            int ordWwmEbv = rdr.GetOrdinal("WWM_EBV");
            int ordMwEbv = rdr.GetOrdinal("MW_EBV");
            int ordRfiEbv = rdr.GetOrdinal("RFI_EBV");
            int ordH18MEbv = rdr.GetOrdinal("H18M_EBV");

            int ordBWEbvRel = rdr.GetOrdinal("BW_EBV_REL");
            int ordWwdEbvRel = rdr.GetOrdinal("WWD_EBV_REL");
            int ordYwtEbvRel = rdr.GetOrdinal("YWT_EBV_REL");
            int ordScEbvRel = rdr.GetOrdinal("SC_EBV_REL");
            int ordBfEbvRel = rdr.GetOrdinal("BF_EBV_REL");
            int ordWwmEbvRel = rdr.GetOrdinal("WWM_EBV_REL");
            int ordMwEbvRel = rdr.GetOrdinal("MW_EBV_REL");
            int ordRfiEbvRel = rdr.GetOrdinal("RFI_EBV_REL");
            int ordH18MEbvRel = rdr.GetOrdinal("H18M_EBV_REL");

            int ordStnADG = rdr.GetOrdinal("Stn_ADG");
            int ordSelIdx = rdr.GetOrdinal("SEL_IDX");

            //int ordBreederDay = rdr.GetOrdinal("BreederDay");
            int ordSaleDate = rdr.GetOrdinal("SaleDate");
            int ordSelectionNum = rdr.GetOrdinal("SelectionNum");
            int ordAccountNo = rdr.GetOrdinal("AccountNo");
            int ordInvNum = rdr.GetOrdinal("InvNum");
            int ordTagColour = rdr.GetOrdinal("TagColour");
            int ordPriceCdn = rdr.GetOrdinal("Price_Cdn");


            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    var saleBull = new SaleBull
                        {
                            Calf_SN = ((int)
                                       ParameterUtils.SafeGetValue(rdr.GetValue(ordCalfSN), typeof (int),
                                                                   Constants.InitializeInt)),
                            Strain_Code = ((string)
                                           ParameterUtils.SafeGetValue(rdr.GetValue(ordStrainCode), typeof (string),
                                                                       Constants.InitializeString)),
                            Herd_Code = ((string)
                                         ParameterUtils.SafeGetValue(rdr.GetValue(ordHerdCode), typeof (string),
                                                                     Constants.InitializeString)),
                            Yr_Code = ((string)
                                       ParameterUtils.SafeGetValue(rdr.GetValue(ordYrCode), typeof (string),
                                                                   Constants.InitializeString)),
                            Calf_Id = ((string)
                                       ParameterUtils.SafeGetValue(rdr.GetValue(ordCalfId), typeof (string),
                                                                   Constants.InitializeString)),
                            Birth_Date = ((DateTime)
                                          ParameterUtils.SafeGetValue(rdr.GetValue(ordBirthDate), typeof (DateTime),
                                                                      Constants.InitializeDateTime)),
                            RawBirth_Wt =
                                (Int16) ParameterUtils.SafeGetValue(rdr.GetValue(ordCalfRawBirthWt), typeof (Int16),
                                                                    Constants.InitializeShort),
                            Feedlot_Id = ((string)
                                          ParameterUtils.SafeGetValue(rdr.GetValue(ordFeedlotId), typeof (string),
                                                                      Constants.InitializeString)),
                            Pen = ((string)
                                   ParameterUtils.SafeGetValue(rdr.GetValue(ordPen), typeof (string),
                                                               Constants.InitializeString)),
                            HideColour_Code = ((string)
                                               ParameterUtils.SafeGetValue(rdr.GetValue(ordHideColourCode),
                                                                           typeof (string),
                                                                           Constants.InitializeString)),
                            Tag_Num = ((int)
                                       ParameterUtils.SafeGetValue(rdr.GetValue(ordTagNum), typeof (int),
                                                                   Constants.InitializeInt)),
                            SireCalf_Id = ((string)
                                           ParameterUtils.SafeGetValue(rdr.GetValue(ordSireCalfId), typeof (string),
                                                                       Constants.InitializeString)),
                            Dam_Id = ((string)
                                      ParameterUtils.SafeGetValue(rdr.GetValue(ordDamId), typeof (string),
                                                                  Constants.InitializeString)),
                            AgeOfDam = ((Int16)
                                        ParameterUtils.SafeGetValue(rdr.GetValue(ordAgeOfDam), typeof (Int16),
                                                                    Constants.InitializeShort)),
                            Dam_Wt = ((Int16)
                                      ParameterUtils.SafeGetValue(rdr.GetValue(ordDamWt), typeof (Int16),
                                                                  Constants.InitializeShort)),
                            Teat = ((byte)
                                    ParameterUtils.SafeGetValue(rdr.GetValue(ordTeat), typeof (byte),
                                                                Constants.InitializeByte)),
                            Udder = ((byte)
                                     ParameterUtils.SafeGetValue(rdr.GetValue(ordUdder), typeof (byte),
                                                                 Constants.InitializeByte)),
                            FF = ((byte)
                                  ParameterUtils.SafeGetValue(rdr.GetValue(ordFf), typeof (byte),
                                                              Constants.InitializeByte)),
                            FL = ((byte)
                                  ParameterUtils.SafeGetValue(rdr.GetValue(ordFl), typeof (byte),
                                                              Constants.InitializeByte)),
                            HF = ((byte)
                                  ParameterUtils.SafeGetValue(rdr.GetValue(ordHf), typeof (byte),
                                                              Constants.InitializeByte)),
                            HL = ((byte)
                                  ParameterUtils.SafeGetValue(rdr.GetValue(ordHl), typeof (byte),
                                                              Constants.InitializeByte)),
                            Morph = ((byte)
                                     ParameterUtils.SafeGetValue(rdr.GetValue(ordMorph), typeof (byte),
                                                                 Constants.InitializeByte)),
                            Motil = ((byte)
                                     ParameterUtils.SafeGetValue(rdr.GetValue(ordMotil), typeof (byte),
                                                                 Constants.InitializeByte)),
                            Conc = ((byte)
                                    ParameterUtils.SafeGetValue(rdr.GetValue(ordConc), typeof (byte),
                                                                Constants.InitializeByte)),
                            Disp = ((byte)
                                    ParameterUtils.SafeGetValue(rdr.GetValue(ordDisp), typeof (byte),
                                                                Constants.InitializeByte)),
                            BW_ADJ = ((Int16)
                                      ParameterUtils.SafeGetValue(rdr.GetValue(ordBWAdj), typeof (Int16),
                                                                  Constants.InitializeShort)),
                            WW_ADJ = ((Int16)
                                      ParameterUtils.SafeGetValue(rdr.GetValue(ordWwAdj), typeof (Int16),
                                                                  Constants.InitializeShort)),
                            YW_ADJ = ((Int16)
                                      ParameterUtils.SafeGetValue(rdr.GetValue(ordYwAdj), typeof (Int16),
                                                                  Constants.InitializeShort)),
                            H18MW_ADJ = ((Int16)
                                         ParameterUtils.SafeGetValue(rdr.GetValue(ordH18MwAdj), typeof (Int16),
                                                                     Constants.InitializeShort)),
                            ADG_BW_ADJ = ((decimal)
                                          ParameterUtils.SafeGetValue(rdr.GetValue(ordADGBWAdj), typeof (decimal),
                                                                      Constants.InitializeDecimal)),
                            BACKFAT_ADJ = ((Int16)
                                           ParameterUtils.SafeGetValue(rdr.GetValue(ordBackfatAdj), typeof (Int16),
                                                                       Constants.InitializeShort)),
                            SCROTCIRC_ADJ = ((decimal)
                                             ParameterUtils.SafeGetValue(rdr.GetValue(ordScrotcircAdj), typeof (decimal),
                                                                         Constants.InitializeDecimal)),
                            BW_EBV = ((decimal)
                                      ParameterUtils.SafeGetValue(rdr.GetValue(ordBWEbv), typeof (decimal),
                                                                  Constants.InitializeDecimal)),
                            WWD_EBV = ((decimal)
                                       ParameterUtils.SafeGetValue(rdr.GetValue(ordWwdEbv), typeof (decimal),
                                                                   Constants.InitializeDecimal)),
                            YWT_EBV = ((decimal)
                                       ParameterUtils.SafeGetValue(rdr.GetValue(ordYwtEbv), typeof (decimal),
                                                                   Constants.InitializeDecimal)),
                            SC_EBV = ((decimal)
                                      ParameterUtils.SafeGetValue(rdr.GetValue(ordScEbv), typeof (decimal),
                                                                  Constants.InitializeDecimal)),
                            BF_EBV = ((decimal)
                                      ParameterUtils.SafeGetValue(rdr.GetValue(ordBfEbv), typeof (decimal),
                                                                  Constants.InitializeDecimal)),
                            WWM_EBV = ((decimal)
                                       ParameterUtils.SafeGetValue(rdr.GetValue(ordWwmEbv), typeof (decimal),
                                                                   Constants.InitializeDecimal)),
                            MW_EBV = ((decimal)
                                      ParameterUtils.SafeGetValue(rdr.GetValue(ordMwEbv), typeof (decimal),
                                                                  Constants.InitializeDecimal)),
                            RFI_EBV = ((decimal)
                                       ParameterUtils.SafeGetValue(rdr.GetValue(ordRfiEbv), typeof (decimal),
                                                                   Constants.InitializeDecimal)),
                            H18M_EBV = ((decimal)
                                        ParameterUtils.SafeGetValue(rdr.GetValue(ordH18MEbv), typeof (decimal),
                                                                    Constants.InitializeDecimal)),
                            BW_EBV_REL = ((decimal)
                                          ParameterUtils.SafeGetValue(rdr.GetValue(ordBWEbvRel), typeof (decimal),
                                                                      Constants.InitializeDecimal)),
                            WWD_EBV_REL = ((decimal)
                                           ParameterUtils.SafeGetValue(rdr.GetValue(ordWwdEbvRel), typeof (decimal),
                                                                       Constants.InitializeDecimal)),
                            YWT_EBV_REL = ((decimal)
                                           ParameterUtils.SafeGetValue(rdr.GetValue(ordYwtEbvRel), typeof (decimal),
                                                                       Constants.InitializeDecimal)),
                            SC_EBV_REL = ((decimal)
                                          ParameterUtils.SafeGetValue(rdr.GetValue(ordScEbvRel), typeof (decimal),
                                                                      Constants.InitializeDecimal)),
                            BF_EBV_REL = ((decimal)
                                          ParameterUtils.SafeGetValue(rdr.GetValue(ordBfEbvRel), typeof (decimal),
                                                                      Constants.InitializeDecimal)),
                            WWM_EBV_REL = ((decimal)
                                           ParameterUtils.SafeGetValue(rdr.GetValue(ordWwmEbvRel), typeof (decimal),
                                                                       Constants.InitializeDecimal)),
                            MW_EBV_REL = ((decimal)
                                          ParameterUtils.SafeGetValue(rdr.GetValue(ordMwEbvRel), typeof (decimal),
                                                                      Constants.InitializeDecimal)),
                            RFI_EBV_REL = ((decimal)
                                           ParameterUtils.SafeGetValue(rdr.GetValue(ordRfiEbvRel), typeof (decimal),
                                                                       Constants.InitializeDecimal)),
                            H18M_EBV_REL = ((decimal)
                                            ParameterUtils.SafeGetValue(rdr.GetValue(ordH18MEbvRel), typeof (decimal),
                                                                        Constants.InitializeDecimal)),
                            Stn_ADG = ((decimal)
                                       ParameterUtils.SafeGetValue(rdr.GetValue(ordStnADG), typeof (decimal),
                                                                   Constants.InitializeDecimal)),
                            SEL_IDX = ((decimal)
                                       ParameterUtils.SafeGetValue(rdr.GetValue(ordSelIdx), typeof (decimal),
                                                                   Constants.InitializeDecimal)),
                            /*
                                                    BreederDay = ((bool)
                                                                  ParameterUtils.SafeGetValue(rdr.GetBoolean(ordBreederDay), typeof (bool), false)),
                        */

                            AccountNo = ((string)
                                         ParameterUtils.SafeGetValue(rdr.GetValue(ordAccountNo), typeof (string),
                                                                     Constants.InitializeString)),
                            InvNum = ((int)
                                      ParameterUtils.SafeGetValue(rdr.GetValue(ordInvNum), typeof (int),
                                                                  Constants.InitializeInt)),
                            Price_Cdn = ((decimal)
                                         ParameterUtils.SafeGetValue(rdr.GetValue(ordPriceCdn), typeof (decimal),
                                                                     Constants.InitializeDecimal)),
                            SaleDate = ((DateTime)
                                        ParameterUtils.SafeGetValue(rdr.GetValue(ordSaleDate), typeof (DateTime),
                                                                    Constants.InitializeDateTime)),
                            SelectionNum = ((int)
                                            ParameterUtils.SafeGetValue(rdr.GetValue(ordSelectionNum), typeof (int),
                                                                        Constants.InitializeInt)),
                            TagColour = ((string)
                                         ParameterUtils.SafeGetValue(rdr.GetValue(ordTagColour), typeof (string),
                                                                     Constants.InitializeString))
                        };

                    lst.Add(saleBull);
                }
            }
            return lst;
        }

        private void AddParameters(SqlCommand sqlCmd, string strain, short saleYear, AvailabilityScope scope, SaleStatusScope saleStatus)
        {
            sqlCmd.Parameters.Add(new SqlParameter
                {
                    Value = strain,
                    ParameterName = "strain",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Char,
                    Size = 2
                });

            sqlCmd.Parameters.Add(new SqlParameter
                {
                    Value = saleYear,
                    ParameterName = "saleYear",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.SmallInt
                });

            sqlCmd.Parameters.Add(new SqlParameter
                {
                    Value = scope,
                    ParameterName = "availabilityScope",
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.SmallInt
                });

            sqlCmd.Parameters.Add(new SqlParameter
            {
                Value = saleStatus,
                ParameterName = "saleStatus",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.SmallInt
            });

        }
    }
}