Hospice Portal README

<--Setup-->
	<System Requirements>
	-An up to date web browser (recent versions of IE, Firefox, and Chrome have all been found to work without issue
	-Microsoft Visual Studio 2013
	-Microsoft SQL Server Express 2014
	-An internet connection is required for certain elements to function properly, like email confirmation as well as some of the sites fonts

	<User Creation>
	-Using the admin page, creating users is a simple form entry process
	-Once a user has been created, a confirmation email will be sent to the user so that password recovery can be enabled. 
	-On a users first log in, they are forced to change their password from the default

	<User Roles>
	-Roles are created and modified via the Admin page
	-Users can be assigned roles when they are created, and can also have them modified by an administrator at any time
	-Records can be assigned roles when they are created, and can also have them modified by an administrator at any time
	-Users will only ever see records that are assigned to their roles

	<Administrative Information>
	-Administrators gain access to the Admin page via the Navigation bar which provides full administrative access to site
	-However for ease of use administrators can create and modify records on the main pages as well
	-Creating records is generally accessed via a plus in the top right of a panel
	-Editing and deleting is always shown to the right of a record and navigates to a seperate page
	-File Assignment (For records that have it) is done during the creation of a record but can be modified at any time by an administrator

	<RSVP's>
	-RSVP's are created and viewable through the administrator page, but users can view pending RSVP's to themselves via the events page
	-Creating an RSVP involves selecting an event, and then all the users to invite to the event

<--Program Overview-->
	<Log-In>
	-Login is handled by entering your account email and password
	-If you forgot your password and have confirmed your email, you can recover it via recovery email

	<Home Page>
	-Features quick views of announcements, upcoming events and schedules relevant to user
	-Clicking on any of these will direct you to the relevant details page
	-Welcome Message is editable by administrator via Admin page
	-Contact Information is accesible at the bottom of the page
	
	<Announcements>
	-Full view of announcements as well as a quick view of upcoming events
	-Announcements only show until their end date
		-To access announcements that have passed their end date use the admin page
	-Clicking on an announcement or an event brings you to the relevant details page
	-Details page of announcements displays any images associated with the announcements and allows the download of any other related files via the panel to the right

	<Resources>
	-All files in the system are accessible and downloadable here
	-Files can be filtered by the various links under the panel headings as well as the search bar
		-The Search bar references both the file description and name
	-When Files are created they can be assigned any of these filter catergories and this can be modified at any time by an administrator
	-More File categories and subcategories can be added via the Admin page
	
	<Events>
	-Events are displayed with more detailed information and a shortened description if nessesary
	-Users can view RSVP requests to themselves via the link at the top of the panel
		-Here you can declare whether you are able to attend as well as add a note to your RSVP
	-Clicking on an event navigates you to the details page of the relevant event
		-Like with announcements, images attached to the event are displayed and other related files are downloadable via the related files panel to the right
	
	<Schedules>
	-Schedules are displayed under their corresponding category
	-Schedule categories can be created via the admin page
	-Schedules are created via the plus at the top of the first panel, just like other records
		-Schedule type is then specified via the dropdown
		-When Creating a schedule, ensure that the Schedule Link is in full 'http://www.link.com' format
	-Clicking on a schedule opens a new tab with the corresponding outlook schedule
		-This schedule will be updated in real time to modifications by the administrator of the calendar (On outlook/office365)
	
	<Contacts>
	-Board contacts and staff contacts are viewable in their own panels
		-Board contacts are created via the plus at the top of the panel like most records, however staff contacts must be created using the admin page
		-Contacts are directly related to users, so when creating a contact you must have the user registered in the system

	<Recently Passed>
	-All notices of death are shown here, as well as a poem that is randomly selected from a table accessible on the admin page
		-When modifying or adding a new poem, ensure to use <br /> to signify a new line

	<Admin Page>
	-Full administrative views of the site data on the main pages are accessible via the Data Maintenance panel
	-User and user roles are viewable and modifiable via the Manage Users panel
	-Less front end data like file categories and death notice poems are accesible via the site maintenece panel
	-The RSVP panel handles all RSVP creation and modification



<--Limitations that require a web developer-->
	
	-Creating or modifying panels other than File Categories and Schedule types
	-Links and contact information on the contact page







<--Copyright BottleRocket Programming 2015-->