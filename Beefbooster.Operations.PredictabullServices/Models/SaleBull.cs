using System;

namespace Beefbooster.Operations.PredictabullServices.Models
{
    public class SaleBull
    {
        public Int32 Calf_SN { get; set; }
        public string Strain_Code { get; set; }
        public string Herd_Code { get; set; }
        public string Yr_Code { get; set; }
        public Int32 Tag_Num { get; set; }
        public string Calf_Id { get; set; }
        public DateTime Birth_Date { get; set; }
        public Int32 RawBirth_Wt { get; set; }
        public string Feedlot_Id { get; set; }
        public string Pen { get; set; }

        public decimal Stn_ADG { get; set; }
        public string HideColour_Code { get; set; }
        public string SireCalf_Id { get; set; }

        public string Dam_Id { get; set; }
        public byte Morph { get; set; }
        public byte Motil { get; set; }
        public byte Conc { get; set; }
        public byte Disp { get; set; }
        public byte FF { get; set; }
        public byte FL { get; set; }
        public byte HF { get; set; }
        public byte HL { get; set; }
      

        public Int32 AgeOfDam { get; set; }
        public short Dam_Wt { get; set; }
        public byte Teat { get; set; }
        public byte Udder { get; set; }


        public decimal BW_ADJ { get; set; }
        public decimal WW_ADJ { get; set; }
        public decimal YW_ADJ { get; set; }
        public decimal H18MW_ADJ { get; set; }
        public decimal ADG_BW_ADJ { get; set; }
        public decimal BACKFAT_ADJ { get; set; }
        public decimal SCROTCIRC_ADJ { get; set; }
        public decimal BW_EBV { get; set; }
        public decimal WWD_EBV { get; set; }
        public decimal YWT_EBV { get; set; }
        public decimal SC_EBV { get; set; }
        public decimal BF_EBV { get; set; }
        public decimal WWM_EBV { get; set; }
        public decimal MW_EBV { get; set; }
        public decimal RFI_EBV { get; set; }
        public decimal H18M_EBV { get; set; }
        public decimal BW_EBV_REL { get; set; }
        public decimal WWD_EBV_REL { get; set; }
        public decimal YWT_EBV_REL { get; set; }
        public decimal SC_EBV_REL { get; set; }
        public decimal BF_EBV_REL { get; set; }
        public decimal WWM_EBV_REL { get; set; }
        public decimal MW_EBV_REL { get; set; }
        public decimal RFI_EBV_REL { get; set; }
        public decimal H18M_EBV_REL { get; set; }

        public decimal SEL_IDX { get; set; }

        //public bool BreederDay { get; set; }
        public DateTime SaleDate { get; set; }
        public Int32 SelectionNum { get; set; }
        public string AccountNo { get; set; }
        public Int32 InvNum { get; set; }
        public string TagColour { get; set; }
        public decimal Price_Cdn { get; set; }
    }
}