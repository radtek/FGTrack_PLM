SELECT
    PARTY_ID,
    PARTY_NAME,
    START_DATE,
    END_DATE,
    STATUS,
    PLAN_QTY,
    CASE ("PROD_TYPE_S")
    WHEN "H" THEN "HOR"
    WHEN "V" THEN "VER"
    WHEN "T" THEN "TAM"
    ELSE "PRS"
    END AS PRODUCT_TYPE,
    PROD_TYPE_S,
    PRODUCT_NO,
    PRODUCT_NAME,
    STOCK_PCS,
    STOCK_BOX,
    MC_NO,
    MACHINE_NAME,
    MIN_BOX,
    MAX_BOX,
    MAX(FLAG)
FROM STOCK_BY_MACHINE
GROUP BY PARTY_ID,
         START_DATE,
         END_DATE,
         MC_NO,
         PARTY_NAME,         
         STATUS,
         PLAN_QTY,
         PROD_TYPE_S,
         PRODUCT_NO,
         PRODUCT_NAME,
         STOCK_PCS,
         STOCK_BOX,         
         MACHINE_NAME,
         MIN_BOX,
         MAX_BOX
ORDER BY MC_NO
;
