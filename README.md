# On Running the Vending Machine

*Before running the .sln file on MS Visual Studio, copy the "VendingMachine" folder contained in this repository and place it into your C:/ Drive. Make sure the name of the folder is the same (ex. not VendingMachineCopy or other). Then you're set!

This is a program written in C# to fulfill the requirements of a capstone project given at the coding bootcamp Tech Elevator, which was meant to demonstrate our understanding of object-oriented programming and file I/O. Its functionality is as follows:

1. Reads a .csv file containing the names, slot identifiers and prices of snacks; storing each snack as an object of type VendingMachineItem and adding it to a list.
2. The list of vending machine items is used to print a display menu to the user.
3. Upon choosing the "Purchase" option from Menu 1, the user can feed the machine USD, adding to their total balance.
4. The user can select items to purchase by slot identifier, subtracting from their balance. 
5. Each item starts out at a quantity of 5, the user cannot purchase an item that is out of stock or that costs more than their current balance.
6. Purchasing a snack returns a consumption message depending on the type of snack.
7. Upon finishing the transaction the user receives change.
8. Each action taken is recorded over the course of each run, and at the end of the transaction they are appended to an existing "Log.txt" file.
9. After ending the transaction the user returns to Menu 1, where there is a hidden menu option (press 9 and ENTER) that creates a new sales audit that is unique by date. This will be generated in the VendingMachine folder that you have copied to your C:/ Drive.
10. Unit testing was done wherever possible.


