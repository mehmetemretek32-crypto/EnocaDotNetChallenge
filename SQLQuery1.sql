-- 1. Kargo Firmasını Tanımla
INSERT INTO Carriers (CarrierName, CarrierIsActive, CarrierPlusDesiCost, CarrierConfigurationId)
VALUES ('Enoca Hızlı Kargo', 1, 10, 1);

-- 2. Fiyatlandırma Aralıklarını Tanımla
-- 1-10 desi arası sabit 50 TL, 11-20 desi arası sabit 80 TL
INSERT INTO CarrierConfigurations (CarrierId, CarrierMaxDesi, CarrierMinDesi, CarrierCost)
VALUES (1, 10, 1, 50.00), (1, 20, 11, 80.00);