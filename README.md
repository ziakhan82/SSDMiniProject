# SSDMiniProject
## Technology Stack

- **Programming Language:** C#
- **Database:** SQL Server
- **Entity Framework (EF):** Entity Framework Core
- **Application Type:** Console Application

- ## Getting Started
- Follow these steps to use the password manager application:

1. **Clone the Repository:**
2. 2. **Set Up the Database:**
- Create a SQL Server database. You can use Microsoft SQL Server Management Studio for this purpose.
- Update the connection string in the `AppDbContext` class to point to your SQl Server. Replace `<connection_string>` with your database connection string.
- run this command  update-database
- build and run the project

 3.**Log In:**
- The application will prompt you to enter your master password.
- If you're running the application for the first time, you have to create a Strong Master Passwrod and it will ask you to confirm your master password.
- For subsequent logins, enter your master password.

  4. **Use the Password Manager:**
- Once logged in, you can use the following commands:
  - `s`: Store a password.
  - `r`: Retrieve a stored password.
  - `q`: Quit the application.
 
## Security Features

1. **Password Strength Requirements:**
   - Strong master passwords are enforced, with criteria like length, uppercase letters, lowercase letters, digits, and special characters. This makes passwords less vulnerable to brute force and dictionary attacks, protecting them from unauthorized attacks
     ![image](https://github.com/ziakhan82/SSDMiniProject/assets/65169811/1fb36a33-44c8-4439-bbfe-c7ca1605b6e3)


2. **Password Hashing:**
   - User master passwords are hashed using the SHA-256 cryptographic hash function before storage. This ensures that, even in a database compromise, the actual passwords remain protected.

3. **Salting:**
   - A random salt is generated for the user. It's combined with the user's master password before hashing, making it harder for attackers to use precomputed tables (rainbow tables) to crack passwords.

4. **Salt Storage:**
   - The salt used for hashing is securely stored alongside the hashed password in the database.
     ![image](https://github.com/ziakhan82/SSDMiniProject/assets/65169811/0776d5e3-d614-4e8f-a572-05c7de93c32d)


5. **User Authentication:**
   - Users must enter their master password for authentication. The application checks this password's hash with the stored hash and salt in the database.
     ![image](https://github.com/ziakhan82/SSDMiniProject/assets/65169811/4548288e-ce68-4f6e-83f8-20d42a4e1769)


6. **Secure Database Storage:**
   - Entity Framework Core is used to interact with the database, abstracting database operations and protecting against SQL injection attacks.

7. **Password Confirmation:**
   - During initial setup, users are asked to confirm their master password to avoid typographical errors, ensuring accurate password creation.
   - ![image](https://github.com/ziakhan82/SSDMiniProject/assets/65169811/809df911-659d-4f58-bf95-db873b2aa8d5)


8. **Error Handling:**
   - The application provides user feedback for incorrect password, making it difficult for attackers to guess passwords.This reduces the risk of information leakage.
   - ![image](https://github.com/ziakhan82/SSDMiniProject/assets/65169811/f8be5c6e-ffd0-4b69-bd9f-57aeb1c23da8)
  
9 **Data Encryption:**
 - The application encrypts credentials before storing them in the database. It uses the Advanced Encryption Standard (AES) symmetric encryption algorithm with a 256-bit key to encrypt the passwords.
 - A unique Initialization Vector (IV) and a randomly generated salt are used for each credential to enhance security. This ensures that even if two passwords are identical, the encrypted result will be different due to the unique IV and salt.
![image](https://github.com/ziakhan82/SSDMiniProject/assets/65169811/de0db92d-7b80-4017-a4b3-0ba1f66ae790)

10 **Key Derivation Function (KDF):**
 - The application employs a Key Derivation Function (KDF) called PBKDF2 (Password-Based Key Derivation Function 2) using the Rfc2898DeriveBytes class to derive the encryption key from the master password and salt. This makes it computationally expensive for attackers to perform brute-force attacks against the stored credentials.

11. **Limited Retrieval Access:**

- The application provides password retrieval only after successful login with the master password. This ensures that credentials are not easily accessible even if someone gains access to the application.
- ![image](https://github.com/ziakhan82/SSDMiniProject/assets/65169811/95f14abe-b299-49e8-b008-3e5109e8d0c3)

    

12. **Enhanced Security Through Cryptographically Secure Randomness:**

- I have chosen to utilize the Rfc2898DeriveBytes class for deriving cryptographic keys from the user's master password and salt. This class internally relies on a Cryptographically Secure Pseudo-Random Number Generator (CSPRNG) to generate secure salts. The use of 
a CSPRNG guarantees the creation of highly unpredictable and unique salts for the user's password. This enhanced level of unpredictability and uniqueness significantly strengthens the security of the encryption process. .
    
13. **Secure Database Connection:**
   - A trusted database connection with SSL/TLS encryption secures data in transit between the application and the database.


## Potential Pitfalls


**Master Password Strength:**
Users need to understand the importance of using strong, unique master passwords.

**Data Backup and Recovery:**
Backup copies of the database should also be encrypted and stored securely.

**Secure Key Management:**
Protect encryption keys and don't store them alongside encrypted data.

**Secure Key Storage:**
Unauthorized access to cryptographic keys can compromise security.

**Monitoring and Intrusion Detection:**
Implement mechanisms to detect and respond to security breaches.

**Software Updates:**
Regularly update the application to address security vulnerabilities.
  
  






