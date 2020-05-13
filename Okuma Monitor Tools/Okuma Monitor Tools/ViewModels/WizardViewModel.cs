using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows;
using Caliburn.Micro;
using Message = System.ServiceModel.Channels.Message;

using Telerik.Windows.Data;


namespace Okuma_Monitor_Tools.ViewModels
{
    
  public  class WizardViewModel: Screen
    {
        // Private backing fields.
        private string _okumaNo;
        private string _plcNo;
        private bool   _networkCheckbox;
        private bool   _plcCheckbox;
        private string _firstOctet;
        private string _secondOctet;
        private string _thirdOctet;
        private string _okumaCom1;
        private string _plcCom1;
        private bool _settingsGrid;
        private bool _wizardFinish;



        /// <summary>
        /// Class Constructor
        /// </summary>
        public WizardViewModel()
        {
            
             // Fill our properties with current settings.
            OkumaNo = My.Settings.OkuamaLast4;
            PlcNo = My.Settings.PlcLast4;
            NetworkCheckbox = My.Settings.NetworkRequired;
            PlcCheckbox = My.Settings.PlcRequired;
            OkumaCom1 = My.Settings.OkumaCom;
            PlcCom1 = My.Settings.PlcCom;

            // Get the Subnet setting and split it up into 3 different octets.
            string source = My.Settings.IPSubnet;
            string[] seperators = new string[] { "." };
            string[] result = source.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            FirstOctet = result[0];
            SecondOctet = result[1];
            ThirdOctet = result[2];

            // Enable the IsHitTextVisible property of the SettingsGrid.
            SettingsGrid = true;
        }

      
        /// <summary>
        /// Tied to OkumaNo Textbox.
        /// </summary>
        public string OkumaNo
        {
            get { return _okumaNo; }
            set
            {
                //ValidateProperty(value, "OkumaNo");
                _okumaNo = value;
                NotifyOfPropertyChange(()=> OkumaNo);
            }
        }


        /// <summary>
        /// Tied to PlcNo Textbox.
        /// </summary>
        public string PlcNo
        {
            get { return _plcNo; }
            set
            {
                //ValidateProperty(value, "PlcNo");
                _plcNo = value; 
                NotifyOfPropertyChange(()=> PlcNo);
            }
        }

        /// <summary>
        /// Tied to NetworkCheckbox Checkbox.
        /// </summary>
        public bool NetworkCheckbox
        {
            get { return _networkCheckbox; }
            set
            {
                _networkCheckbox = value; 
                NotifyOfPropertyChange(()=> NetworkCheckbox);
            }
        }

        /// <summary>
        /// Tied to PlcCheckbox Checkbox.
        /// </summary>
        public bool PlcCheckbox
        {
            get { return _plcCheckbox; }
            set
            {
                _plcCheckbox = value;
                NotifyOfPropertyChange(()=> PlcCheckbox);
            }
        }



        /// <summary>
        /// Tied to FirstOctet Textbox.
        /// </summary>
        public string FirstOctet
        {
            get { return _firstOctet; }
            set
            {
                _firstOctet = value; 
                NotifyOfPropertyChange(()=> FirstOctet);
            }
        }

        /// <summary>
        /// Tied to SecondOctet Textbox.
        /// </summary>
        public string SecondOctet
        {
            get { return _secondOctet; }
            set
            {
                _secondOctet = value; 
                NotifyOfPropertyChange(()=> SecondOctet);
            }
        }

        /// <summary>
        /// Tied to ThirdOctet Textbox.
        /// </summary>
        public string ThirdOctet
        {
            get { return _thirdOctet; }
            set
            {
                _thirdOctet = value; 
                NotifyOfPropertyChange(()=> ThirdOctet);
            }
        }

        /// <summary>
        /// Tied to OkumaCom1 Textbox.
        /// </summary>
        public string OkumaCom1
        {
            get { return _okumaCom1; }
            set { _okumaCom1 = value;
                NotifyOfPropertyChange(()=> OkumaCom1);
            }
        }

        /// <summary>
        /// Tied to PlcCom1 Textbox.
        /// </summary>
        public string PlcCom1
        {
            get { return _plcCom1; }
            set
            {
                _plcCom1 = value;
                NotifyOfPropertyChange(()=> PlcCom1);
            }
        }


        /// <summary>
        /// Tied to SettingsGrid IsHitTestVisible property.
        /// </summary>
        public bool SettingsGrid
        {
            get { return _settingsGrid; }
            set
            {
                _settingsGrid = value;
                NotifyOfPropertyChange(()=> SettingsGrid);
            }
        }






        /// <summary>
        /// Monitors the input parameters (properties). When the data passes all the checks
        /// it will return true, this allows the Save Settings button to be Enabled.
        /// </summary>
        /// <param name="okumaNo"></param>
        /// <param name="plcNo"></param>
        /// <param name="networkCheckbox"></param>
        /// <param name="plcCheckbox"></param>
        /// <param name="firstOctet"></param>
        /// <param name="secondOctet"></param>
        /// <param name="thirdOctet"></param>
        /// <param name="okumaCom1"></param>
        /// <param name="plcCom1"></param>
        /// <returns></returns>
        public bool CanSaveSettings(string okumaNo, string plcNo, bool networkCheckbox, bool plcCheckbox, string firstOctet,
            string secondOctet, string thirdOctet, string okumaCom1, string plcCom1)
        {
           return ValidateSettings(okumaNo, plcNo, networkCheckbox, plcCheckbox, firstOctet,
                secondOctet, thirdOctet, okumaCom1, plcCom1);
            
        }

        /// <summary>
        /// Tied to the SaveSettings button. The parameters are required to match the parameters
        /// of the CanSave parameters. the parameters are not required to be used in this method.
        /// </summary>
        /// <param name="okumaNo"></param>
        /// <param name="plcNo"></param>
        /// <param name="networkCheckbox"></param>
        /// <param name="plcCheckbox"></param>
        /// <param name="firstOctet"></param>
        /// <param name="secondOctet"></param>
        /// <param name="thirdOctet"></param>
        /// <param name="okumaCom1"></param>
        /// <param name="plcCom1"></param>
        public void SaveSettings(string okumaNo, string plcNo, bool networkCheckbox, bool plcCheckbox, string firstOctet,
            string secondOctet, string thirdOctet, string okumaCom1, string plcCom1)
        {
            string subNet = $"{FirstOctet}.{SecondOctet}.{ThirdOctet}";
            My.Settings.IPSubnet = subNet;
            My.Settings.OkumaCom = okumaCom1;
            My.Settings.PlcCom = plcCom1;
            My.Settings.PlcLast4 = plcNo;
            My.Settings.OkuamaLast4 = okumaNo;
            My.Settings.PlcRequired = plcCheckbox;
            My.Settings.NetworkRequired = NetworkCheckbox;
            My.Settings.Wizard = false;

            // Make the SettingsGrid IsHitTestVisible = to false.
            SettingsGrid = false;

            // Enable the Wizard Finish button.
            WizardFinish = true;

            // Save the Settings.
            My.Settings.Write();
            MessageBox.Show("Settings have been saved, press the Finish button and restart the application.");

        }

        /// <summary>
        /// This method validates all the user input data. If all the checks pass the method returns true.
        /// </summary>
        /// <param name="okumaNo"></param>
        /// <param name="plcNo"></param>
        /// <param name="networkCheckbox"></param>
        /// <param name="plcCheckbox"></param>
        /// <param name="firstOctet"></param>
        /// <param name="secondOctet"></param>
        /// <param name="thirdOctet"></param>
        /// <param name="okumaCom"></param>
        /// <param name="plcCom"></param>
        /// <returns></returns>
        private bool ValidateSettings(string okumaNo, string plcNo, bool networkCheckbox, bool plcCheckbox,
            string firstOctet,
            string secondOctet, string thirdOctet, string okumaCom, string plcCom)
        {
            // Set our Output to true.
            bool output = true;

           
            // Variables to store integer values after parsed from our strings.
            int outOkumaNo = 0;
            int outPlcNo = 0;
            int outFirstOctet = 0;
            int outSecondOctet = 0;
            int outThirdOctet = 0;
            int outOkumaCom = 0;
            int outPlcCom = 0;

            // bool variable for our TryParse.
            bool canConvert;

            // If our string has at least one char, do an int.TryParse. If the string can be converted 
            // into an integer the value will be stored in the integer variables.
            if (!string.IsNullOrWhiteSpace(okumaNo))
            {
                canConvert = int.TryParse(okumaNo.ToString(), out outOkumaNo);

                // If it cannot be converted then the method fails and returns false.
                if (canConvert == false || okumaNo.Length != 4)

                {
                    output = false;
                }
            }

           
            // If the Network check box is checked then we need to check all the network settings.
            if (networkCheckbox == true)
            {
                if (string.IsNullOrWhiteSpace(firstOctet)
                    || string.IsNullOrWhiteSpace(secondOctet)
                    || string.IsNullOrWhiteSpace(thirdOctet)
                    || string.IsNullOrWhiteSpace(okumaCom))
                {
                    output = false;
                }

                canConvert = int.TryParse(firstOctet.ToString(), out outFirstOctet);

                // Check to make sure the values are within the valid IO addressing range.
                if (canConvert == false || outFirstOctet < 1 || outFirstOctet > 254)
                {
                    output = false;
                }
                canConvert = int.TryParse(secondOctet.ToString(), out outSecondOctet);

                if (canConvert == false || outSecondOctet < 1 || outSecondOctet > 254)
                {
                    output = false;
                }

                canConvert = int.TryParse(thirdOctet.ToString(), out outThirdOctet);

                if (canConvert == false || outThirdOctet < 1 || outThirdOctet > 254)
                {
                    output = false;
                }
                canConvert = int.TryParse(okumaCom.ToString(), out outOkumaCom);

                if (canConvert == false || outOkumaCom < 1 || outOkumaCom > 254)
                {
                    output = false;
                }
               
            }
            
            // If the PLC checkbox is checked we have to verify the PLC data.
            if (plcCheckbox== true)
            {
                if (string.IsNullOrWhiteSpace(plcNo) || string.IsNullOrWhiteSpace(plcCom))
                {
                    output = false;
                }

                canConvert = int.TryParse(plcNo, out outPlcNo);

                if (canConvert == false || plcNo.Length != 4)
                {
                    output = false;
                }

                canConvert = int.TryParse(plcCom.ToString(), out outPlcCom);

                if (canConvert == false || outPlcCom < 1 || outPlcCom > 254)
                {
                    output = false;
                }   
            }
            
            // Output will be true unless any of the checks fail.
            return output;
        }


        #region WizardPage
        
        /// <summary>
        /// Tied to the Wizards Finish button.
        /// </summary>
        public bool WizardFinish
        {
            get { return _wizardFinish; }
            set
            {
                _wizardFinish = value;
                NotifyOfPropertyChange(()=> WizardFinish);
            }
        }
        #endregion

    }
}
