<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"
 xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" >

  <xsl:template match="/">
    <AccountInquiryResponse>
      <xsl:choose>
        <xsl:when test="//ResponseStatus">
          <errCode>
            <xsl:value-of select="//ResponseStatus/ResponseStatusCode/."/>
          </errCode>
          <errDesc>
            <xsl:value-of select="//ResponseStatus/ResponseStatusDescription/."/>
          </errDesc>
        </xsl:when>
        <xsl:otherwise>
          <errCode>0</errCode>
          <errDesc>OK</errDesc>
        </xsl:otherwise>
      </xsl:choose>
      <AccountNo>
        <xsl:value-of select="//GetDepositAccountDetailsRs/DepAcctProdRec/BankProdId/ProdId"/>
      </AccountNo>
      
      <Type>
        <xsl:value-of select="//GetDepositAccountDetailsRs/DepAcctProdRec/BankProdId/ProdType/Desc"/>
      </Type>

      <Status>
        <xsl:value-of select="//GetDepositAccountDetailsRs/DepAcctProdRec/BankProdId/ProdStatus/Desc"/>
      </Status>

      <Currency>
        <xsl:value-of select="//GetDepositAccountDetailsRs/DepAcctProdRec/BankProdId/ProdCur/CurCode/Desc"/>
      </Currency>

      <CurrencyCode>
        <xsl:value-of select="//GetDepositAccountDetailsRs/DepAcctProdRec/BankProdId/ProdCur/CurCode/Val"/>
      </CurrencyCode>
      
      <OpenDate>
        <xsl:value-of select="//GetDepositAccountDetailsRs/DepAcctProdRec/DepAcctProdInfo/OpenDt"/>
      </OpenDate>
      
      <Language>
        <xsl:value-of select="//GetDepositAccountDetailsRs/DepAcctProdRec/DepAcctProdInfo/Language/Desc"/>
      </Language>
       <StopIndicator>
        <xsl:value-of select="//GetDepositAccountDetailsRs/DepAcctProdRec/DepAcctProdInfo/StopInd"/>
      </StopIndicator>

      <StatementCycle>
        <xsl:value-of select="//GetDepositAccountDetailsRs/DepAcctProdRec/StmtInfo/StmtFreq/Desc"/>
      </StatementCycle>
      <StatementCycleCode>
        <xsl:value-of select="//GetDepositAccountDetailsRs/DepAcctProdRec/StmtInfo/StmtFreq/Val"/>
      </StatementCycleCode>
      
      <CurrentBalance>
        <xsl:value-of select="//GetDepositAccountDetailsRs/DepAcctProdRec/ProdBal[BalType='Current']/CurAmt/Amt"/>
      </CurrentBalance>
      <EffectiveBalance>
        <xsl:value-of select="//GetDepositAccountDetailsRs/DepAcctProdRec/ProdBal[BalType='EffectiveBalance']/CurAmt/Amt"/>
      </EffectiveBalance>
      <AvailableBalance>
        <xsl:value-of select="//GetDepositAccountDetailsRs/DepAcctProdRec/ExtProdBal[ExtBalType='Available']/CurAmt/Amt"/>
      </AvailableBalance>
      <HoldAmount>
        <xsl:value-of select="//GetDepositAccountDetailsRs/DepAcctProdRec/HoldInfo[HoldType='Total']/Amt"/>
      </HoldAmount>
    </AccountInquiryResponse>
  </xsl:template>


</xsl:stylesheet>

