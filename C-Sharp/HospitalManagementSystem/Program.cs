using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagementSystem;
namespace dbmsApp1
{
    class HospitalMain
    {
        static void Main(string[] args)
        {
            HospitalMain hospitalMain = new HospitalMain();
            hospitalMain.Run();
        }
        public void Run()
        {
            Console.WriteLine("Welcome to Hospital Management System");
            Console.WriteLine("Choose the operation you want to perform");
            Console.WriteLine("Available operations are:");
            Console.WriteLine("1. Display operations");
            Console.WriteLine("2. Insert operations");
            Console.WriteLine("3. Update operations");
            Console.WriteLine("4. Delete operations");
            Console.WriteLine("5. exit");
            Console.WriteLine("Enter your choice (1-5):");
            int choice = Convert.ToInt32(Console.ReadLine());
            Performtask performtask = new Performtask();
            performtask.PerformOperation(choice);
            //HospitalManagementSystem.Hospital_Packages.DbConnection.DisplayConnectionHospitalDB();
            //HospitalManagementSystem.Hospital_Packages.DisplayPatients.DisplayPatientsInfo();
            //HospitalManagementSystem.Hospital_Packages.DisplayDoctors.DisplayDoctorsInfo();
            //HospitalManagementSystem.Hospital_Packages.DisplayPatients.DisplaTotalPatientsCount();
            //HospitalManagementSystem.Hospital_Packages.DisplayDoctors.DisplaTotalDoctorCount();
        }
    }
    internal class Performtask
    {
        public void PerformOperation(int operation)
        {
            InsertOperations insertOperations = new InsertOperations();
            UpdateOperations updateOperations = new UpdateOperations();
            DeleteOperations deleteOperations = new DeleteOperations();
            DisplayOperations displayOperations = new DisplayOperations();
            switch (operation)
            {
                case 1:
                    displayOperations.DisplayOperationMenu();
                    break;
                case 2:
                    insertOperations.InsertOperationMenu();
                    break;
                case 3:
                    updateOperations.UpdateOperationMenu();
                    break;
                case 4:
                    deleteOperations.DeleteOperationMenu();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            // Fix: Call PerformOperation recursively instead of undefined Run()
            Console.WriteLine("\nReturning to main menu...\n");
            HospitalMain hospitalMain = new HospitalMain();
            hospitalMain.Run();
        }
    }
    class DisplayOperations
    {
        public void DisplayOperationMenu()
        {
            Console.WriteLine(
                "Choose the display operation you want to perform\n" +
                "1. Display all patients\n" +
                "2. Display all doctors\n" +
                "3. Display total number of patients\n" +
                "4. Display total number of doctors\n" +
                "5. Display Patient by Patient ID\n" +
                "6. Display Doctor by Doctor ID\n" +
                "7. Display all Patients for same disease\n" +
                "8. Display all Doctors for same specialization\n" +
                "9. Display all Appointments for a Patient ID\n" +
                "10. Display all Appointments for a Doctor ID\n" +
                "11. Display most consulted doctor\n"+
                "Enter your choice (1-11):");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    HospitalManagementSystem.Hospital_Packages.DisplayPatients.DisplayPatientsInfo();
                    break;
                case 2:
                    HospitalManagementSystem.Hospital_Packages.DisplayDoctors.DisplayDoctorsInfo();
                    break;
                case 3:
                    HospitalManagementSystem.Hospital_Packages.DisplayPatients.DisplaTotalPatientsCount();
                    break;
                case 4:
                    HospitalManagementSystem.Hospital_Packages.DisplayDoctors.DisplaTotalDoctorCount();
                    break;
                case 5:
                    HospitalManagementSystem.Hospital_Packages.DisplayPatients.DisplayPatientByID();
                    break;
                case 6:
                    HospitalManagementSystem.Hospital_Packages.DisplayDoctors.DisplayDoctorByID();
                    break;
                case 7:
                    HospitalManagementSystem.Hospital_Packages.DisplayPatients.DisplayPatientsByDisease();
                    break;
                case 8:
                    HospitalManagementSystem.Hospital_Packages.DisplayDoctors.DisplayDoctorsBySpecialization();
                    break;
                case 9:
                    HospitalManagementSystem.Hospital_Packages.DisplayAppointments.DisplayAppointmentsByPatientID();
                    break;
                case 10:
                    HospitalManagementSystem.Hospital_Packages.DisplayAppointments.DisplayAppointmentsByDoctorID();
                    break;
                case 11:
                    HospitalManagementSystem.Hospital_Packages.DisplayDoctors.DisplayMostConsultedDoctor();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            return;
        }
    }
    class InsertOperations
    {
        public void InsertOperationMenu()
        {
            Console.WriteLine(
                "Choose the insert operation you want to perform\n" +
                "1. Insert a new patient\n" +
                "2. Insert a new doctor\n" +
                "3. Insert a new appointment\n" +
                //"4. Insert a new department\n" +
                //"5. Insert a new treatment\n" +
                //"6. Insert a new medicine\n" +
                //"7. Insert a new room\n" +
                //"8. Insert a new bill\n" +
                "Enter your choice (1-3):");
            int choice = Convert.ToInt32(Console.ReadLine());
            HospitalManagementSystem.Hospital_Packages.InsertOperations insertOperations = new HospitalManagementSystem.Hospital_Packages.InsertOperations();
            switch (choice)
            {
                case 1:
                    insertOperations.insertPatient();
                    break;
                case 2:
                    insertOperations.insertDoctor();
                    break;
                case 3:
                    insertOperations.insertAppointment();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            return;
        }
    }
    class UpdateOperations
    {
        public void UpdateOperationMenu()
        {
            Console.WriteLine(
                "Choose the update operation you want to perform\n" +
                "1. Update patient details\n" +
                "2. Update doctor details\n" +
                //"3. Update appointment details\n" +
                //"4. Update department details\n" +
                //"5. Update treatment details\n" +
                //"6. Update medicine details\n" +
                //"7. Update room details\n" +
                //"8. Update bill details\n" +
                "Enter your choice (1-2):");
            int choice = Convert.ToInt32(Console.ReadLine());
            HospitalManagementSystem.Hospital_Packages.UpdateOperations updateOperations = new HospitalManagementSystem.Hospital_Packages.UpdateOperations();
            switch (choice)
            {
                case 1:
                    updateOperations.updatePatient();
                    break;
                case 2:
                    updateOperations.updateDoctor();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            return;
        }
    }
    class DeleteOperations
    {
        public void DeleteOperationMenu()
        {
            Console.WriteLine(
                "Choose the delete operation you want to perform\n" +
                "1. Delete a patient\n" +
                "2. Delete a doctor\n" +
                "3. Delete/Cancel an appointment\n" +
                //"4. Delete a department\n" +
                //"5. Delete a treatment\n" +
                //"6. Delete a medicine\n" +
                //"7. Delete a room\n" +
                //"8. Delete a bill\n" +
                "Enter your choice (1-3):");
            int choice = Convert.ToInt32(Console.ReadLine());
            HospitalManagementSystem.Hospital_Packages.DeleteOperations deleteOperations = new HospitalManagementSystem.Hospital_Packages.DeleteOperations();
            switch (choice)
            {
                case 1:
                    deleteOperations.deletePatient();
                    break;
                case 2:
                    deleteOperations.deleteDoctor();
                    break;
                case 3:
                    deleteOperations.deleteAppointment();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            return;
        }
    }
}