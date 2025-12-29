$token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2ZjBjMTM3Ni0zMDNkLTQ2ZjYtOTNlMC1kYjE1NjA2MTUzYzgiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwiZW1haWwiOiJhZG1pbkBpbnZlbnRvcnkuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFkbWluaXN0cmFkb3IgZG8gU2lzdGVtYSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwianRpIjoiZDZlMmY3ODctMDVlNi00MmJlLTk5YmItYTk0OGJjOTk3NzhjIiwiZXhwIjoxNzY2Nzg1MTMwLCJpc3MiOiJJbnZlbnRvcnlNYW5hZ2VtZW50QVBJIiwiYXVkIjoiSW52ZW50b3J5TWFuYWdlbWVudENsaWVudHMifQ.zQyqiza2YIBjl9XFysA2l0Tjt5XT1OCNzEpj3-foWh0"

$suppliers = @(
    @{name="Tech Solutions Brasil Ltda"; tradeName="Tech Solutions"; cnpj="12345678000190"; city="Sao Paulo"; state="SP"}
    @{name="Informatica Total ME"; tradeName="Info Total"; cnpj="23456789000191"; city="Rio de Janeiro"; state="RJ"}
    @{name="Eletronicos Modernos S.A."; tradeName="Eletronicos Modernos"; cnpj="34567890000192"; city="Belo Horizonte"; state="MG"}
    @{name="Distribuidora Alpha Ltda"; tradeName="Alpha"; cnpj="45678901000193"; city="Curitiba"; state="PR"}
    @{name="MegaTech Importadora"; tradeName="MegaTech"; cnpj="56789012000194"; city="Porto Alegre"; state="RS"}
    @{name="Computadores e Cia"; tradeName="Computadores Cia"; cnpj="67890123000195"; city="Brasilia"; state="DF"}
    @{name="Suprimentos Office Ltda"; tradeName="Office Supply"; cnpj="78901234000196"; city="Recife"; state="PE"}
    @{name="Global Tech Brasil"; tradeName="Global Tech"; cnpj="89012345000197"; city="Salvador"; state="BA"}
    @{name="NextGen Tecnologia"; tradeName="NextGen"; cnpj="90123456000198"; city="Fortaleza"; state="CE"}
    @{name="Smart Devices Ltda"; tradeName="Smart Devices"; cnpj="01234567000199"; city="Manaus"; state="AM"}
    @{name="Premium Informatica"; tradeName="Premium Info"; cnpj="11223344000100"; city="Goiania"; state="GO"}
    @{name="Digital World S.A."; tradeName="Digital World"; cnpj="22334455000101"; city="Belem"; state="PA"}
    @{name="TechnoPlus Distribuidora"; tradeName="TechnoPlus"; cnpj="33445566000102"; city="Vitoria"; state="ES"}
    @{name="Componentes Brasil Ltda"; tradeName="Componentes BR"; cnpj="44556677000103"; city="Florianopolis"; state="SC"}
    @{name="InfoSystem Comercio"; tradeName="InfoSystem"; cnpj="55667788000104"; city="Campinas"; state="SP"}
    @{name="Hardware Center Ltda"; tradeName="HW Center"; cnpj="66778899000105"; city="Sao Bernardo"; state="SP"}
    @{name="Tecnologia Avancada ME"; tradeName="Tech Avancada"; cnpj="77889900000106"; city="Ribeirao Preto"; state="SP"}
    @{name="Suprimentos Tech S.A."; tradeName="Sup Tech"; cnpj="88990011000107"; city="Santos"; state="SP"}
    @{name="MicroInfo Distribuidora"; tradeName="MicroInfo"; cnpj="99001122000108"; city="Sorocaba"; state="SP"}
    @{name="Eletronica Pro Ltda"; tradeName="Eletronica Pro"; cnpj="10111213000109"; city="Guarulhos"; state="SP"}
    @{name="TechMaster Brasil"; tradeName="TechMaster"; cnpj="20212223000110"; city="Osasco"; state="SP"}
    @{name="Inovacao Digital Ltda"; tradeName="Inovacao Digital"; cnpj="30313233000111"; city="Niteroi"; state="RJ"}
    @{name="Conecta Tech S.A."; tradeName="Conecta"; cnpj="40414243000112"; city="Uberlandia"; state="MG"}
    @{name="Solucoes TI Ltda"; tradeName="Solucoes TI"; cnpj="50515253000113"; city="Londrina"; state="PR"}
    @{name="Mega Suprimentos"; tradeName="Mega Sup"; cnpj="60616263000114"; city="Joinville"; state="SC"}
    @{name="Tech Express Ltda"; tradeName="Tech Express"; cnpj="70717273000115"; city="Caxias do Sul"; state="RS"}
    @{name="Importadora Brasil Tech"; tradeName="BR Tech"; cnpj="80818283000116"; city="Maringa"; state="PR"}
    @{name="Distribuidor Nacional S.A."; tradeName="Dist Nacional"; cnpj="90919293000117"; city="Aracaju"; state="SE"}
    @{name="TechParts Comercio"; tradeName="TechParts"; cnpj="10203040000118"; city="Maceio"; state="AL"}
    @{name="Digital Commerce Ltda"; tradeName="Digital Commerce"; cnpj="20304050000119"; city="Joao Pessoa"; state="PB"}
)

$counter = 1
foreach ($supplier in $suppliers) {
    $emailDomain = $supplier.tradeName.ToLower().Replace(' ', '')
    $json = "{`"name`":`"$($supplier.name)`",`"tradeName`":`"$($supplier.tradeName)`",`"cnpj`":`"$($supplier.cnpj)`",`"email`":`"contato@$emailDomain.com.br`",`"phone`":`"(11) 9$counter$counter$counter$counter-$counter$counter$counter$counter`",`"contactPerson`":`"Joao Silva $counter`",`"address`":{`"street`":`"Rua Exemplo $counter`",`"number`":`"${counter}00`",`"complement`":`"Sala $counter`",`"neighborhood`":`"Centro`",`"city`":`"$($supplier.city)`",`"state`":`"$($supplier.state)`",`"postalCode`":`"01310-$($counter.ToString('000'))`",`"country`":`"Brasil`"},`"currency`":`"BRL`"}"

    Write-Host "Criando fornecedor $counter : $($supplier.name)..." -ForegroundColor Cyan

    try {
        $headers = @{
            "Authorization" = "Bearer $token"
            "Content-Type" = "application/json"
        }

        $response = Invoke-RestMethod -Uri "http://localhost:5000/api/Suppliers" -Method POST -Headers $headers -Body $json
        Write-Host "OK Fornecedor criado! ID: $($response.id)" -ForegroundColor Green
    }
    catch {
        Write-Host "ERRO ao criar fornecedor: $($_.Exception.Message)" -ForegroundColor Red
    }

    $counter++
    Start-Sleep -Milliseconds 100
}

Write-Host ""
Write-Host "Processo concluido! $($suppliers.Count) fornecedores criados." -ForegroundColor Green
