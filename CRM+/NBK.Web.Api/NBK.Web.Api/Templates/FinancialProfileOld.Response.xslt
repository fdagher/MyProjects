<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"
 xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" >
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
  <xsl:template match="/">
    <FinancialProfileResponse>
      <xsl:choose>
        <xsl:when test="//ResponseStatus">
          <ErrCode>
            <xsl:value-of select="//ResponseStatus/ResponseStatusCode/."/>
          </ErrCode>
          <ErrDesc>
            <xsl:value-of select="//ResponseStatus/ResponseStatusDescription/."/>
          </ErrDesc>
        </xsl:when>
        <xsl:otherwise>
          <ErrCode>0</ErrCode>
          <ErrDesc>OK</ErrDesc>
        </xsl:otherwise>
      </xsl:choose>

      <TotalCashLimit>
        <xsl:value-of select="//CustProfInfo/TotalCashLimit/Amt"/>
      </TotalCashLimit>

      <TotalNonCashLimit>
        <xsl:value-of select="//CustProfInfo/TotalNonCashLimit/Amt"/>
      </TotalNonCashLimit>

      <TotalEarnedAssets>
        <xsl:value-of select="//CustProfInfo/TotalEarnedAssets/Amt"/>
      </TotalEarnedAssets>

      <TotalFreeFunds>
        <xsl:value-of select="//CustProfInfo/TotalFreeFunds/Amt"/>
      </TotalFreeFunds>

      <TotalCashLiability>
        <xsl:value-of select="//CustProfInfo/TotalCashLiability/Amt"/>
      </TotalCashLiability>

      <TotalIndirectLiability>
        <xsl:value-of select="//CustProfInfo/TotalIndirectLiability/Amt"/>
      </TotalIndirectLiability>

      <TotalNonCashLiability>
        <xsl:value-of select="//CustProfInfo/TotalNonCashLiability/Amt"/>
      </TotalNonCashLiability>

      <TotalCollateral>
        <xsl:value-of select="//CustProfInfo/TotalCollateral/Amt"/>
      </TotalCollateral>

      <TotalNBKFunds>
        <xsl:value-of select="//CustProfInfo/TotalNBKFunds/Amt"/>
      </TotalNBKFunds>

    </FinancialProfileResponse>
  </xsl:template>
</xsl:stylesheet>

