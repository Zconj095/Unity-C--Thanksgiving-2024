# NavigationManager

## Overview
The `NavigationManager` script is responsible for managing the visibility of different UI panels within a Unity application. It allows the user to switch between three panels: the dashboard, user, and service panels. This script is essential for creating a smooth user experience by ensuring that only the relevant panel is displayed at any given time, thus keeping the interface clean and organized.

## Variables
- `public GameObject dashboardPanel`: A reference to the dashboard panel GameObject. This panel displays the main dashboard interface to the user.
- `public GameObject userPanel`: A reference to the user panel GameObject. This panel provides user-specific functionalities and information.
- `public GameObject servicePanel`: A reference to the service panel GameObject. This panel displays various services available to the user.

## Functions
- `public void ShowPanel(string panelName)`: This function takes a string parameter `panelName` and activates the corresponding panel while deactivating the others. It checks the value of `panelName` and sets the active state of `dashboardPanel`, `userPanel`, and `servicePanel` accordingly. If `panelName` is "dashboard", the dashboard panel is shown; if it's "user", the user panel is displayed; and if it's "service", the service panel is activated. This function is the core of the navigation logic, enabling seamless transitions between different sections of the user interface.