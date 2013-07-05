using System;
using System.Collections.Generic;
using System.Linq;
using EServ.Shared;
using System.Text;

namespace InfoPos.Parameter
{
    public class Main
    {
        private Core.Core _core;

        public Main()
        {

        }
        //Документийн бүлгийн бүртгэл
        public void CallFormDocTypeGroup(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormDocTypeGroup frm = new InfoPos.Parameter.FormDocTypeGroup(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //Динамик тайлангийн бүлэг
        public void CallFormReportGroup(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormReportGroup frm = new InfoPos.Parameter.FormReportGroup(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //CRM Төслийн төрөл
        public void CallCRMProjectType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.CRMProjectType frm = new InfoPos.Parameter.CRMProjectType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //CRM Асуудлын төрөл
        public void CallCRMIssueTypes(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.CRMIssueTypes frm = new InfoPos.Parameter.CRMIssueTypes(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //CRM Асуудлын алхамууд
        public void CallCRMIssueTracks(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.CRMIssueTrack frm = new InfoPos.Parameter.CRMIssueTrack(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //CRM Асуудлын үйлдлийн төрөл
        public void CallCRMIssueActionType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.CRMIssueActionType frm = new InfoPos.Parameter.CRMIssueActionType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //CRM Асуудлын хаагдсан төрөл
        public void CallCRMIssueResolutionType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.CRMIssueResolutionType frm = new InfoPos.Parameter.CRMIssueResolutionType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //CRM Асуудлын холбоотой хүний үүргийн төрөл
        public void CallCRMIssueMemberPurp(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.CRMIssueMemberPurp frm = new InfoPos.Parameter.CRMIssueMemberPurp(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //CRM Асуудлын эрэмбэ
        public void CallCRMIssuePriority(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.CRMIssuePriority frm = new InfoPos.Parameter.CRMIssuePriority(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        //CRM Асуудлын холбоосын төрөл
        public void CallCRMIssueLinkType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.CRMIssueLinkType frm = new InfoPos.Parameter.CRMIssueLinkType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //CRM Төслийн дэд төрөл
        public void CallCRMIssueProjectComp(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.CRMIssueProjectComp frm = new InfoPos.Parameter.CRMIssueProjectComp(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //CRM Төсөл
        public void CallCRMIssueProject(object[] param)
        {
            _core = (Core.Core)param[0];
            long projectid = 0;
            try
            {
                projectid = Static.ToLong(param[1]);
            }
            catch { projectid = 0; }
            InfoPos.Parameter.CRMIssueProject frm = new InfoPos.Parameter.CRMIssueProject(_core, projectid);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //CRM Мэдэгдэлийн схем болон гүйлгээ
        public void CallCRMIssueNotifyTxn(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.CRMNotifyTxn frm = new InfoPos.Parameter.CRMNotifyTxn(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //CRM Эрхийн схем болон гүйлгээ
        public void CallCRMIssuePermTxn(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.CRMPermTxn frm = new InfoPos.Parameter.CRMPermTxn(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //Документ загварын бүртгэл
        public void CallFormDocTemplate(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormDocTemplate frm = new InfoPos.Parameter.FormDocTemplate(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //Shortcut бүртгэл
        public void CallFormShortcut(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormShortcut frm = new InfoPos.Parameter.FormShortcut(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //Dictionary жагсаалт
        public void CallFormDictionary(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormDictionary dic = new InfoPos.Parameter.FormDictionary(_core);
            dic.MdiParent = _core.MainForm;
            dic.Show();
        }
        //Dynamic жагсаалтs
        public void CallFormDynamicList(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormDynamicList dic = new InfoPos.Parameter.FormDynamicList(_core);
            dic.MdiParent = _core.MainForm;
            dic.Show();
        }
        // Үндсэн хөрөнгийн байршилын бүртгэл
        public void CallFaPosition(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FaPosition fas = new InfoPos.Parameter.FaPosition(_core);
            fas.MdiParent = _core.MainForm;
            fas.Show();
        }

        public void CallContractAdd(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormContractAdd frm = new InfoPos.Parameter.FormContractAdd(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallCustomerAdd(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormCustomerAdd frm = new InfoPos.Parameter.FormCustomerAdd(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallFormGenParam(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormGenParam frm = new InfoPos.Parameter.FormGenParam(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        //public void CallRiskGroup(object[] param)
        //{
        //    _core = (Core.Core)param[0];
        //    InfoPos.Parameter.RiskGroup frm = new InfoPos.Parameter.RiskGroup(_core);
        //    frm.MdiParent = _core.MainForm;
        //    frm.Show();
        //}


        public void CallBranch(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormBranch frm = new InfoPos.Parameter.FormBranch(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallCountry(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormCountry frm = new InfoPos.Parameter.FormCountry(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallLanguage(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormLanguage frm = new InfoPos.Parameter.FormLanguage(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallAge(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormAge frm = new InfoPos.Parameter.FormAge(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallCurrency(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormCurrency frm = new InfoPos.Parameter.FormCurrency(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallBank(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormBank frm = new InfoPos.Parameter.FormBank(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallCustomerType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormCustomerType frm = new InfoPos.Parameter.FormCustomerType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallAutoNum(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormAutoNum frm = new InfoPos.Parameter.FormAutoNum(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallFamilyType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormFamilyType frm = new InfoPos.Parameter.FormFamilyType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallIndustry(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormIndustry frm = new InfoPos.Parameter.FormIndustry(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallSubIndustry(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormSubIndustry frm = new InfoPos.Parameter.FormSubIndustry(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallCustCity(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormCustCity frm = new InfoPos.Parameter.FormCustCity(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallCustRate(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormCustRate frm = new InfoPos.Parameter.FormCustRate(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallCustDistrict(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormCustDistrict frm = new InfoPos.Parameter.FormCustDistrict(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallCustSubDistrict(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormCustSubDistrict frm = new InfoPos.Parameter.FormCustSubDistrict(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        //public void CallExtensyType(object[] param) 
        //{
        //    _core = (Core.Core)param[0];
        //    InfoPos.Parameter.FormExtenseType frm = new InfoPos.Parameter.FormExtenseType(_core);
        //    frm.MdiParent = _core.MainForm;
        //    frm.Show();
        //}

        public void CallCloseType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormCloseType frm = new InfoPos.Parameter.FormCloseType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }


        public void CallBankBranch(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormBankBranch frm = new InfoPos.Parameter.FormBankBranch(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallUnitType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormUnitType frm = new InfoPos.Parameter.FormUnitType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallInventoryType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormInventoryType frm = new InfoPos.Parameter.FormInventoryType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallBacProduct(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormBacProduct frm = new InfoPos.Parameter.FormBacProduct(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallConProduct(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormConProduct frm = new InfoPos.Parameter.FormConProduct(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallFormCustomerMask(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormCustomerMask frm = new InfoPos.Parameter.FormCustomerMask(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallFormFAType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormFAType frm = new InfoPos.Parameter.FormFAType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        //public void CallFormClaimCloseType(object[] param) 
        //{
        //    _core = (Core.Core)param[0];
        //    InfoPos.Parameter.FormClaimCloseType frm = new InfoPos.Parameter.FormClaimCloseType(_core);
        //    frm.MdiParent = _core.MainForm;
        //    frm.Show();
        //}

        //public void CallFormClaimObjectType(object[] param)
        //{
        //    _core = (Core.Core)param[0];
        //    InfoPos.Parameter.FormClaimObjectType frm = new InfoPos.Parameter.FormClaimObjectType(_core);
        //    frm.MdiParent = _core.MainForm;
        //    frm.Show();
        //}

        public void CallFormStepItem(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormStepItem frm = new InfoPos.Parameter.FormStepItem(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallFormStep(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormStep frm = new InfoPos.Parameter.FormStep(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallFormAccountCode(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormAccountCode frm = new InfoPos.Parameter.FormAccountCode(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallFormTxn(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormTxn frm = new InfoPos.Parameter.FormTxn(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallTxnEntry(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormTxnWrite frm = new InfoPos.Parameter.FormTxnWrite(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallFormTxnEntry(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormTxnEntry frm = new InfoPos.Parameter.FormTxnEntry(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        //public void CallPaProduct(object[] param)
        //{
        //    _core = (Core.Core)param[0];
        //    int Prodcode = 0;
        //    try
        //    {
        //        Prodcode = Static.ToInt(param[1]);
        //    }
        //    catch { Prodcode = 0; }
        //    InfoPos.Parameter.PaProduct frm = new InfoPos.Parameter.PaProduct(_core, Prodcode);
        //    frm.MdiParent = _core.MainForm;
        //    frm.Show();
        //}

        public void CallFormFormula(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormFormula frm = new InfoPos.Parameter.FormFormula(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallFormCustContactType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormCustContactType frm = new InfoPos.Parameter.FormCustContactType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallFormBrokerCompany(object[] param)
        {
            _core = (Core.Core)param[0];
            long Brokerid = 0;
            try
            {
                Brokerid = Static.ToLong(param[1]);
            }
            catch { Brokerid = 0; }
            InfoPos.Parameter.FormBrokerCompany frm = new InfoPos.Parameter.FormBrokerCompany(_core, Brokerid);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        //Since 2012:06:08
        public void CallFormPaDaytype(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormPaDaytype frm = new InfoPos.Parameter.FormPaDaytype(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallFormPAWeather(object[] param) //Цаг агаарын төрлийн бүртгэл
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormPAWeather frm = new InfoPos.Parameter.FormPAWeather(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallFormPAInvType(object[] param) //Бараа материалын төрлийн бүртгэл
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormPAInvType frm = new InfoPos.Parameter.FormPAInvType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallPATagSetup(object[] param) //Тагийн төрлийн бүртгэл
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.PATagSetup frm = new InfoPos.Parameter.PATagSetup(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallFormPAServType(object[] param) //Үйлчилгээний төрлийн бүртгэл
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.PAServType frm = new InfoPos.Parameter.PAServType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallFormPaBill(object[] param) // Мөнгөн тэмдэгт  
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormPaBill frm = new InfoPos.Parameter.FormPaBill(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallFormPAPayType(object[] param) // Төлбөрийн төрөл
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormPAPayType frm = new InfoPos.Parameter.FormPAPayType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallFormPabrand(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.PaBrand frm = new InfoPos.Parameter.PaBrand(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallFormPaScheduleType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.PaScheduleType frm = new InfoPos.Parameter.PaScheduleType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallFormInvMain(object[] param) //Барааны бүртгэл
        {
            string pInvID = "";
            _core = (Core.Core)param[0];
            try
            {
                pInvID = Static.ToStr(param[1]);
            }
            catch { pInvID = ""; }
            InfoPos.Parameter.FormInvMain frm = new InfoPos.Parameter.FormInvMain(_core, pInvID);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallFormPaInvCat(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.PaInvCat frm = new InfoPos.Parameter.PaInvCat(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }  //Холбуулах 
        public void CallFormServMain(object[] param) //Барааны бүртгэл
        {
            string pServID = "";
            _core = (Core.Core)param[0];
            try
            {
                pServID = Static.ToStr(param[1]);
            }
            catch { pServID = ""; }
            InfoPos.Parameter.ServMain frm = new InfoPos.Parameter.ServMain(_core, pServID);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallPaContractType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormPaContractType frm = new InfoPos.Parameter.FormPaContractType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallPosTerminal(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormPosTerminal frm = new InfoPos.Parameter.FormPosTerminal(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
            //-ПОС-ын бүртгэл
            //-FormPosTerminal
            //-CallPosTerminal
            //-140078
        }
        public void CallFormPackMain(object[] param)
        {
            string pPackID = "";
            _core = (Core.Core)param[0];
            try
            {
                pPackID = Static.ToStr(param[1]);
            }
            catch { pPackID = ""; }
            InfoPos.Parameter.FormPackMain frm = new InfoPos.Parameter.FormPackMain(_core, pPackID);
            frm.MdiParent = _core.MainForm;
            frm.Show();



            //-Бүтээгдэхүүний багцын бүртгэл
            //-FormPackMain
            //-CallFormPackMain
            //-140248
        }

        public void CallPaProductTree(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormPaProductTree frm = new InfoPos.Parameter.FormPaProductTree(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
            //-ПОС-ын бүртгэл
            //-FormPosTerminal
            //-CallPosTerminal
            //-140078
        }
        public void CallPaProductTreeDesc(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormPaProductTreeDesc frm = new InfoPos.Parameter.FormPaProductTreeDesc(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
            //-ПОС-ын бүртгэл
            //-FormPosTerminal
            //-CallPosTerminal
            //-140078
        }
        public void CallPADamageType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormPADamageType frm = new InfoPos.Parameter.FormPADamageType(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallPledgeType(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormPledgetype frm = new InfoPos.Parameter.FormPledgetype(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallRebateMaster(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormRebateMaster frm = new InfoPos.Parameter.FormRebateMaster(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }
        public void CallRebateDetail(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormRebateDetail frm = new InfoPos.Parameter.FormRebateDetail(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallRebateFormula(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormRebateFormula frm = new InfoPos.Parameter.FormRebateFormula(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallXlsReport(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormReport frm = new InfoPos.Parameter.FormReport(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

        public void CallTagMain(object[] param)
        {
            _core = (Core.Core)param[0];
            InfoPos.Parameter.FormTagMain frm = new InfoPos.Parameter.FormTagMain(_core);
            frm.MdiParent = _core.MainForm;
            frm.Show();
        }

    }
}