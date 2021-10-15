# Nasa-Rover


## **-Introduction:**
This Web App retrieves images from NASA Mars Rover API by sending a request with the date as a parameter (The dates are read from a text file and the rover name). The images are stored locally and then displayed.

## -The Structure:

### -Communication:
This folder contains the class HttpCaller.cs which contains a HTTP Call function that submits the http GET request and receive the response and converts the JSON response into objects.

### -Controllers:
This folder caontains main controllers:
 1. Initializing the rovers {curiosity, opportunity, spirit}.
 2. GetImages: this functions returns list of images for the selected dates in the text file and the rover name.
 
 ### -Helpers:
 This folder contains the class DateHelper.cs. This class contains a couple of functions:
 1. ValidateDatesFromFile: Checks if the date in the file is valid or not.
 2. ParseDate: This function parses the date into the following date forat {yyyy-MM-dd}.
 
  ### -Models:
This folder contains the properties for the objects we want to use in the web application. There are four main classes:

1. CallResponse.cs
2. ErrorViewModel.cs
3. ParsedDate.cs
4. ResponseViewModel.cs

   ### -Services:
This folder coontains the class NasaWebServices.cs. This class is responsible for fetching the requested images with a valid date and then  downloading them and storing them locally.

  ### -Data:
This folder contains the date file we are going to read from, and it is the location where the images are going to be downloaded to and read from.
