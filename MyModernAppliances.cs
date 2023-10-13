/***************************************************************************
 //File:		MyModernAppliances.cs
 //Author:      Jubril Somide, Peng Winko, Anowai Samuel, Kevin Ly
 //Date: 	    10/13/2023
 //Description:	Manager class for Modern Appliances
****************************************************************************/
using ModernAppliances.Entities;
using ModernAppliances.Entities.Abstract;
using ModernAppliances.Helpers;
using System.Transactions;

namespace ModernAppliances
{
    /// <summary>
    /// Manager class for Modern Appliances
    /// </summary>
    /// <remarks>Author: </remarks>
    /// <remarks>Date: </remarks>
    internal class MyModernAppliances : ModernAppliances
    {
        /// <summary>
        /// Option 1: Performs a checkout
        /// </summary>
        public override void Checkout()
        {
            Console.WriteLine("Enter the item number of an appliance:");

            long itemNumber;

            // Get user input for the item number.
            string userInput = Console.ReadLine();  
            itemNumber = long.Parse(userInput);

            // foundAppliance will hold the appliance found in the list.
            Appliance foundAppliance = null;

            // Loop through Appliances
            foreach(Appliance appliance in Appliances)
            {
                if (appliance.ItemNumber == itemNumber) // Test appliance item number equals entered item number
                {
                    foundAppliance = appliance;
                    break;
                }
            }
            if(foundAppliance == null)
            {
                Console.WriteLine($"No appliances found with that item number.");
            }
            else
            {
                if (foundAppliance.IsAvailable) // Test appliance was not found (foundAppliance is null)
                {
                    foundAppliance.Checkout();
                    Console.WriteLine($"Appliance \"{itemNumber}\" has been checked out.");
                }
                else
                {
                    Console.WriteLine($"The appliance is not available to be checked out.");
                }
            }
           
        }

        /// <summary>
        /// Option 2: Finds appliances
        /// </summary>
        public override void Find()
        {
            Console.WriteLine("Enter brand to search for:");
            // Create string variable to hold entered brand
            string brandName = Console.ReadLine();

            List<Appliance> foundAppliances = new List<Appliance>();

            foreach(Appliance appliance in Appliances) // Iterate through loaded appliances
            {
                if (appliance.Brand.ToLower() == brandName.ToLower())
                {
                    foundAppliances.Add(appliance);
                }
            }

            DisplayAppliancesFromList(foundAppliances, foundAppliances.Count); // Display found appliances
  
        }

        /// <summary>
        /// Displays Refridgerators
        /// </summary>
        public override void DisplayRefrigerators()
        {
            Console.WriteLine("Enter number of doors: 2 (double dooor), 3 (three door), 4 (four doors):");

            // Create variable to hold entered number of doors
            int numberOfDoors = int.Parse(Console.ReadLine());
            List<Appliance> foundAppliances = new List<Appliance>();

            foreach(Appliance appliance in Appliances) // Iterate through loaded appliances
            {
                if (appliance is Refrigerator) // Test that current appliance is a refrigerator
                {
                    Refrigerator refrigerator = (Refrigerator)appliance;

                    // Test user entered 0 or refrigerator doors equals what user entered.
                    if (numberOfDoors == 0 || refrigerator.Doors == numberOfDoors) 
                    {
                        foundAppliances.Add(appliance);
                    }
                }
            }
            DisplayAppliancesFromList(foundAppliances, foundAppliances.Count); // Display found appliances
            
        }

        /// <summary>
        /// Displays Vacuums
        /// </summary>
        /// <param name="grade">Grade of vacuum to find (or null for any grade)</param>
        /// <param name="voltage">Vacuum voltage (or 0 for any voltage)</param>
        public override void DisplayVacuums()
        {
            Console.WriteLine("Enter battery voltage value. 18 V (low) or 24 V (high):");
            // Create variable to hold entered voltage
            string voltage = Console.ReadLine();
            if(voltage == "18")
            {
                voltage = "low";
            }
            else if(voltage == "24")
            {
                voltage = "high";
            }
            else
            {
                Console.WriteLine("Invalid option.");

                return;
            }

            List<Appliance> foundAppliances = new List<Appliance>();
            
            foreach(Appliance appliance in Appliances) // Iterate through loaded appliances
            {
                if (appliance is Vacuum)
                {
                    Vacuum vacuum = (Vacuum)appliance;

                    // Test grade is "Any" or grade is equal to current vacuum grade and voltage is 0 or voltage is equal to current vacuum voltage
                    if (voltage == "low" & vacuum.BatteryVoltage == 18)
                    {
                        foundAppliances.Add(appliance);
                    }
                    else if (voltage == "high" & vacuum.BatteryVoltage == 24)
                    {
                        foundAppliances.Add(appliance);
                    }
                }
            }
            DisplayAppliancesFromList(foundAppliances, foundAppliances.Count); // Display found appliances
            
        }

        /// <summary>
        /// Displays microwaves
        /// </summary>
        public override void DisplayMicrowaves()
        {
            Console.WriteLine("Room where the microwave will be installed: K (kitchen) or W (work site):");
            // Create variable to hold entered room type
            string userInput = Console.ReadLine().ToUpper();
            char roomType;

            switch (userInput)
            {
                case "K":
                    roomType = 'K';
                    break;
                case "W":
                    roomType = 'W';
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    return;
            }
            
            List<Appliance> foundAppliances = new List<Appliance>();

            // Loop through Appliances
            foreach(Appliance appliance in Appliances)
            {
                if (appliance is Microwave)
                {
                    Microwave microwave = (Microwave)appliance;

                    if (roomType == 'K' & microwave.RoomType == 'K') // Test room type equals 'K' or microwave room type
                    {
                        foundAppliances.Add(appliance);
                    }
                    else if (roomType == 'W' & microwave.RoomType == 'W') // Test room type equals 'W' or microwave room type
                    {
                        foundAppliances.Add(appliance);
                    }
                }
            }
            DisplayAppliancesFromList(foundAppliances, foundAppliances.Count); // Display found appliances
            
        }

        /// <summary>
        /// Displays dishwashers
        /// </summary>
        
        public override void DisplayDishwashers()
        {
            Console.WriteLine("Enter the sound rating of the dishwasher: Qt (Quietest), Qr (Quieter), Qu(Quiet) or M (Moderate):");
            // Create variable to hold entered sound rating
            string soundRating = Console.ReadLine().ToLower();
            
            switch(soundRating) // Test sound rating equals "Qt", "Qr", "Qu" or "M"
            {
                case "qt":
                    soundRating = "Quietest";
                    break;
                case "qr":
                    soundRating = "Quieter";
                    break;
                case "qu":
                    soundRating = "Quiet";
                    break;
                case "m":
                    soundRating = "Moderate";
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    return;
            }

            List<Appliance> foundAppliances = new List<Appliance>();

            // Iterate through loaded appliances
            foreach (Appliance appliance in Appliances) 
            {
                if (appliance is Dishwasher) // Test current appliance is a dishwasher
                {
                    Dishwasher dishwasher = (Dishwasher)appliance;
                    if (soundRating == "Quietest" & dishwasher.SoundRating == "Qt")
                    {
                        foundAppliances.Add(appliance);
                    }
                    else if (soundRating == "Quieter" & dishwasher.SoundRating == "Qr")
                    {
                        foundAppliances.Add(appliance);
                    }
                    else if (soundRating == "Quiet" & dishwasher.SoundRating == "Qu")
                    {
                        foundAppliances.Add(appliance);
                    }
                    else if (soundRating == "Moderate" & dishwasher.SoundRating == "M")
                    {
                        foundAppliances.Add(appliance);
                    }
                }
            }

            DisplayAppliancesFromList(foundAppliances, foundAppliances.Count); // Display found appliances

        }

        /// <summary>
        /// Generates random list of appliances
        /// </summary>
        public override void RandomList()
        {
            Console.WriteLine("Enter number of appliances:");

            // Create variable to hold entered number of appliances
            int numberOfAppliances = int.Parse(Console.ReadLine());

            List<Appliance> foundAppliances = new List<Appliance>();

            foreach(Appliance appliance in Appliances) // Iterate through loaded appliances
            {
                foundAppliances.Add(appliance);
            }

            foundAppliances.Sort(new RandomComparer()); // Randomize list of found appliances

            DisplayAppliancesFromList(foundAppliances, numberOfAppliances); // Display found appliances (up to max. number inputted)
            
        }
    }
}
