<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0"
 xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" >
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
  <xsl:template match="/">
    <CustomerInquiryResponse>
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

     
        
      <FullName>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CustName/FullName"/>
      </FullName>
      <ShortName>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CustName/ShortName"/>
      </ShortName>
      <TitleCode>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CustName/TitlePrefix/Val"/>
      </TitleCode>
      <Title>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CustName/TitlePrefix/Desc"/>
      </Title>
      <StatusCode>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CustStatus/Val"/>
      </StatusCode>
      <Status>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CustStatus/Desc"/>
      </Status>
      <LanguageCode>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CustPref/Language/Val"/>
      </LanguageCode>
      <Language>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CustPref/Language/Desc"/>
      </Language>
      
       <PackageCode>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CustPref/PackageInfo/PackageType/Val"/>
      </PackageCode>
      <Package>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CustPref/PackageInfo/PackageType/Desc"/>
      </Package>
      
       <CountryOfBirthCode>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CountryOfBirth/Val"/>
      </CountryOfBirthCode>
      <CountryOfBirth>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CountryOfBirth/Desc"/>
      </CountryOfBirth>
      
       <OriginCountryCode>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/OriginCountry/Val"/>
      </OriginCountryCode>
      
      <OriginCountry>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/OriginCountry/Desc"/>
      </OriginCountry>

     

      <BirthDate>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/BirthDt"/>
      </BirthDate>
      <Gender>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/Gender"/>
      </Gender>
      <NationalityCode>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/Nationality/Val"/>
      </NationalityCode>
      <Nationality>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/Nationality/Desc"/>
      </Nationality>
      <MaritalStatusCode>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/MaritalStatus/Val"/>
      </MaritalStatusCode>
      <MaritalStatus>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/MaritalStatus/Desc"/>
      </MaritalStatus>
      <EducationCode>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/Education/Val"/>
      </EducationCode>
      <Education>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/Education/Desc"/>
      </Education>

    

     <IdentityInfo>
      <ResidenceStatusCode>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/GovIssueIdent/ResidenceStatus/Val"/>
      </ResidenceStatusCode>
      <ResidenceStatus>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/GovIssueIdent/ResidenceStatus/Desc"/>
      </ResidenceStatus>
      
      <IdentTypeCode>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/GovIssueIdent/GovIssueIdentType/Val"/>
      </IdentTypeCode>
      <IdentType>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/GovIssueIdent/GovIssueIdentType/Desc"/>
      </IdentType>
      <IdentNumber>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/GovIssueIdent/IdentSerialNum"/>
      </IdentNumber>
      <IdentExpiryDate>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/GovIssueIdent/ExpDt"/>
      </IdentExpiryDate>

      <IdIssuePlaceCode>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/GovIssueIdent/IdIssuePlace/Val"/>
      </IdIssuePlaceCode>
      <IdIssuePlace>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/GovIssueIdent/IdIssuePlace/Desc"/>
      </IdIssuePlace>
     </IdentityInfo>
      
      <ContactInfo>
        <HomePhone>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/ContactInfo/PhoneNum[PhoneType/Val='01']/Phone"/>
       </HomePhone>
        <Mobile>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/ContactInfo/PhoneNum[PhoneType/Val='04']/Phone"/>
        </Mobile>
        <Email>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/ContactInfo/EmailAddr"/>
        </Email>
      </ContactInfo>
      
       <BankInfo>
         <EstablishDate>
            <xsl:value-of select="//CustInqRs/CustRec/EstablishDt"/>
         </EstablishDate>
        <BranchCode>
            <xsl:value-of select="//CustInqRs/CustRec/CustInfo/BankInfo/BranchId"/>
        </BranchCode>
        <BranchName>
            <xsl:value-of select="//CustInqRs/CustRec/CustInfo/BankInfo/ExtBranchId/Property[Name='Description']/Val"/>
        </BranchName>
      </BankInfo>
           
      <CustTypeInfo>
        <CustTypeCode>
           <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CustTypeInfo/CustType/Val"/>
        </CustTypeCode>
        <CustType>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CustTypeInfo/CustType/Desc"/>
        </CustType>
        <GroupType>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CustTypeInfo/CustGroupType"/>
        </GroupType>
      </CustTypeInfo>
      
      <PackageTypeCode>
        <xsl:value-of select="//CustInqRs/CustRec/CustInfo/CustPref/PackageInfo/PackageType/Val"/>
      </PackageTypeCode>
      
      <HomeAddress>
        <AreaCode>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='01']/Area/Val"/>
        </AreaCode>
        <Area>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='01']/Area/Desc"/>
        </Area>
        <Block>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='01']/AddrInfo/BlockNum"/>
        </Block>
        <Street>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='01']/AddrInfo/Street"/>
        </Street>
         <BldgPlot>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='01']/AddrInfo/BldgPlot"/>
        </BldgPlot>
         <Avenue>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='01']/AddrInfo/Avenue"/>
        </Avenue>
        <UnitNum>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='01']/AddrInfo/UnitNum"/>
        </UnitNum>
        <FloorNum>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='01']/AddrInfo/FloorNum"/>
        </FloorNum>
        <Addr1>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='01']/Addr1"/>
        </Addr1>
        <Addr2>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='01']/Addr2"/>
        </Addr2>
        <Addr3>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='01']/Addr3"/>
        </Addr3>
        <Addr4>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='01']/Addr4"/>
        </Addr4>
      </HomeAddress>

      <WorkAddress>
        <AreaCode>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='02']/Area/Val"/>
        </AreaCode>
        <Area>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='02']/Area/Desc"/>
        </Area>
        <Addr1>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='02']/Addr1"/>
        </Addr1>
        <Addr2>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='02']/Addr2"/>
        </Addr2>
        <Addr3>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='02']/Addr3"/>
        </Addr3>
        <Addr4>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/ContactInfo/PostAddr[AddrType/Val='02']/Addr4"/>
        </Addr4>
      </WorkAddress>
      
    <EmploymentInfo>
        <WorkplaceCode>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/EmploymentHistory/Workplace/Val"/>
        </WorkplaceCode>
        <Workplace>
           <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/EmploymentHistory/Workplace/Desc"/>
        </Workplace>
        <OccupationCode>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/EmploymentHistory/Occupation/Val"/>
        </OccupationCode>
        <Occupation>
           <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/EmploymentHistory/Occupation/Desc"/>
        </Occupation>
      
        <JobRankCode>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/EmploymentHistory/JobRank/Val"/>
        </JobRankCode>
        <JobRank>
           <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/EmploymentHistory/JobRank/Desc"/>
        </JobRank>
    </EmploymentInfo>
     <IncomeInfo>
        <IncomeSourceCode>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/IncomeInfo/IncomeSource/Val"/>
        </IncomeSourceCode>
        <IncomeSource>
           <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/IncomeInfo/IncomeSource/Desc"/>
        </IncomeSource>
     
        <SalaryRangeCode>
          <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/IncomeInfo/SalaryRange/Val"/>
        </SalaryRangeCode>
        <SalaryRange>
           <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/IncomeInfo/SalaryRange/Desc"/>
        </SalaryRange>
        <Salary>
           <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/IncomeInfo/Salary/Amt"/>
        </Salary>
        <Wealth>
           <xsl:value-of select="//CustInqRs/CustRec/CustInfo/PersonInfo/IncomeInfo/Salary/Amt"/>
        </Wealth>
    </IncomeInfo>
      
    </CustomerInquiryResponse>
  </xsl:template>
</xsl:stylesheet>

