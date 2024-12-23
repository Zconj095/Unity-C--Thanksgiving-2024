# VRControllerEncryption

## Overview
The `VRControllerEncryption` script is responsible for managing the encryption and decryption of data from VR controllers in a Unity application. It retrieves the position and rotation of each connected VR controller, encrypts this data for secure transmission or storage, and then decrypts it to demonstrate the process. This script fits within the broader context of a VR application where secure handling of controller data is essential for maintaining user privacy and data integrity.

## Variables
- `devices`: A list of `InputDevice` objects that represent all VR controllers connected to the system. This list is populated by querying the system for devices with the characteristics of a controller.
- `position`: A `Vector3` variable that stores the 3D position of the VR controller obtained from the device.
- `rotation`: A `Quaternion` variable that stores the rotational orientation of the VR controller obtained from the device.
- `rawData`: A string that concatenates the position and rotation data into a single string for encryption.
- `encryptedData`: A string that holds the encrypted version of `rawData`, generated by the `AESEncryption.Encrypt` method.
- `decryptedData`: A string that holds the decrypted version of `encryptedData`, generated by the `AESEncryption.Decrypt` method.

## Functions
- `Update()`: This is a Unity lifecycle method that is called once per frame. It retrieves all VR controllers, checks their position and rotation, encrypts this data, and then decrypts it to demonstrate the encryption process. It logs both the encrypted and decrypted data to the console for verification.