using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("HTN.BITS.UIL.PLASESS")]
[assembly: AssemblyDescription("Assembly For User Interface")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("HI-TECH NITTSU (THAILAND) CO.,LTD.")]
[assembly: AssemblyProduct("FINISHED GOODS TRACKING")]
[assembly: AssemblyCopyright("Copyright © HI-TECH NITTSU (THAILAND) 2011")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("d654d83f-653f-4b8b-9651-cb5e91c9fa77")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.8.0")]
[assembly: AssemblyFileVersion("1.0.8.0")]
//1.0.2.14 Improve Performent for Retrieve Data and Change the PickingList Report
//1.0.2.15 Fix Bug in Stock Out Detail : Missing outdate parameter.
//1.0.2.16 Improve Performent : Show warning Message Print Times.
//1.0.2.17 Update Print Label and Stock in History
//1.0.2.18 Update Print Label for Customer Return.
//1.0.2.19 Fix bug and add new requirement.
//1.0.3.0 Add new module about Details On Pallet and Improvment code logic.
//1.0.3.1 Fix bug create customer Return.
//1.0.3.2 Fix bug Enable add new button on Shipping Order.
//1.0.3.3 Fix Stock In Summary Very Slow.
//1.0.3.4 Fix Stock Out Summary and Production Summary Daily Very Slow.
//1.0.3.5 Modified for Pallet Detail and Save Default W/H and default Date Query
//1.0.3.6 Update new request ment and fixbug on 17-Jan-2014.
//1.0.3.7 fixbug details on pallets on 17-Jan-2014.
//1.0.3.8 Unknow
//1.0.3.9 Improvement for Press Production
//1.0.4.0 Fix bug in Stock in Summary Report
//1.0.4.1 Fix bug in Stock in Summary Report (forgot status R = QC RETURN)
//1.0.4.2 Modified Stock As On (Very slow) 25-07-2014
//1.0.4.3 Modified Stock As On double click show detail any cell 28-07-2014 10:40am
//1.0.4.4 Enable Stock As On Date 01-Aug-2014 03:40 pm
//1.0.4.5 Improvement PO#
//1.0.4.6 Unlock ETA Date
//1.0.4.7 Add new Export Complete Shipping Order to CSV
//1.0.4.8 Add new column for CSV File User ID and Email 19-Sep-2014
//1.0.4.9 Add new column Packaging
//1.0.5.0 Fix bug when compare summary qty of Qty and PickQty
//1.0.5.1 Modified Transfer Order for Export CSV File
//1.0.5.2 Modified Plan Auto 06-02-2015
//1.0.5.3 Fix Bug Plan Auto 07-02-2015
//1.0.5.4 Modified Upload Plan Auto for use Machine differance id by Toey on 10-02-2015 9:50 am
//1.0.5.5 Modified Post Data Condition to Qty = AssignQty by Jack on 09-03-2015 11:30 am
//1.0.5.6 Fix bug in Shipping Order and Picking List by Jack on 06-05-2015 17:00 pm
//1.0.5.7 Add new Material Module by Toey on 18-12-2015 10:00 am
//1.0.5.8 Fixbug and chaqnge little bit for Press (Screen and Report) by Toey on 23-12-2015 10:30 am
//1.0.5.9 Fixbug Report (Screen and Report) by Toey on 23-12-2015 13:30 am
//1.0.6.0 FixSize Report (Screen and Report) by Toey on 23-12-2015 14:30 am
//1.0.6.1 Add mtl in, mtl out, change label press layout (Screen and Report) by Toey on 25-12-2015 11:30 am
//1.0.6.2 Fix Generate btn disable wrong status by Toey on 04-01-2016 17:30 pm
//1.0.6.3 Add print picking copy to PRESS (Party Id = 'PST008') by Toey on 26-01-2016 10:30 
//1.0.6.4 Add sending and receiving on picking form and add relation between product and party on Job Order and Shiping Order  by Toey on 01-02-2016 09:30 
//1.0.6.5 Fixed unplanin relation between product and party   by Toey on 02-02-2016 10:00 
//1.0.6.6 Fixed bug by Toey on 02-02-2016 10:30
//1.0.6.7 Fixed Print label PRESS by Toey on 24-02-2016 11:15
//1.0.6.8 Fixed Print label PRESS by Toey on 24-02-2016 12:45
//1.0.6.9 Fixed Print label PRESS by Toey on 24-02-2016 13:00
//1.0.7.0 Fixed Print label PRESS by Toey on 25-02-2016 11:20
//1.0.7.1 Fixed Print label PRESS by Toey on 25-02-2016 15:30
//1.0.7.2 Fixed Print label INJ by Toey on 25-02-2016 17:30
//1.0.7.3 Fixed Print label INJ by Toey on 26-02-2016 14:30
//1.0.7.4 Fixed Print label INJ by Toey on 26-02-2016 14:30
//1.0.7.5 Fixed Print label INJ by Toey on 26-02-2016 16:00
//1.0.7.6 Fixed Print label PRESS, Add search wh on ARR_HDR by Toey on 01-03-2016 11:15
//1.0.7.7 Fixed Stock in out summary int to decimal and add grade , color by Toey on 04-03-2016 17:45
//1.0.7.8 Fixed format on label by Toey on 14-03-2016 11:45
//1.0.7.9 Change barcode from Image to DevExpress Barcode and disable alert when export SO Detial to Excel by Jack & Tangmo on 25-03-2016 13:00
//1.0.8.0 Change FGTrack to MAPLe by Jack on 23-04-2016