-- MySQL dump 10.13  Distrib 8.0.20, for macos10.15 (x86_64)
--
-- Host: 127.0.0.1    Database: MYPARKING
-- ------------------------------------------------------
-- Server version	5.7.41

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Clients`
--

DROP TABLE IF EXISTS `Clients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Clients` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Plate` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `AdmissionDateTime` datetime NOT NULL,
  `PlaceId` int(11) NOT NULL,
  `DepatureDateTime` datetime DEFAULT NULL,
  `Discount` float DEFAULT NULL,
  `VehicleTypeId` int(11) NOT NULL,
  `ElectricHybrid` tinyint(1) NOT NULL,
  `TotalPay` double DEFAULT NULL,
  `State` tinyint(1) NOT NULL COMMENT 'false=outstanding receivable; true= charged',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Clients`
--

LOCK TABLES `Clients` WRITE;
/*!40000 ALTER TABLE `Clients` DISABLE KEYS */;
/*!40000 ALTER TABLE `Clients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Places`
--

DROP TABLE IF EXISTS `Places`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Places` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `VehicleTypeId` int(11) NOT NULL,
  `Available` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Places`
--

LOCK TABLES `Places` WRITE;
/*!40000 ALTER TABLE `Places` DISABLE KEYS */;
INSERT INTO `Places` VALUES (1,'C1',2,1),(2,'C2',2,1),(3,'C3',2,1),(4,'C4',2,1),(5,'C5',2,1),(6,'M1',1,1),(7,'M2',1,1),(8,'M3',1,1),(9,'M4',1,1),(10,'M5',1,1),(11,'M6',1,1);
/*!40000 ALTER TABLE `Places` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `VechicleType`
--

DROP TABLE IF EXISTS `VechicleType`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `VechicleType` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Rate` decimal(10,0) NOT NULL COMMENT 'Parking hour rate',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `VechicleType`
--

LOCK TABLES `VechicleType` WRITE;
/*!40000 ALTER TABLE `VechicleType` DISABLE KEYS */;
INSERT INTO `VechicleType` VALUES (1,'Motorcycle',62),(2,'Light Vehicle',120);
/*!40000 ALTER TABLE `VechicleType` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'MYPARKING'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-05-12  3:17:23
