$token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2ZjBjMTM3Ni0zMDNkLTQ2ZjYtOTNlMC1kYjE1NjA2MTUzYzgiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwiZW1haWwiOiJhZG1pbkBpbnZlbnRvcnkuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFkbWluaXN0cmFkb3IgZG8gU2lzdGVtYSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwianRpIjoiNmYzZmIxM2QtYTkyYi00YmU0LWE1M2MtYjRkODNjZmE2NzM1IiwiZXhwIjoxNzY2Nzg3MTg0LCJpc3MiOiJJbnZlbnRvcnlNYW5hZ2VtZW50QVBJIiwiYXVkIjoiSW52ZW50b3J5TWFuYWdlbWVudENsaWVudHMifQ.cbjXTMO6-JYkZzcfDTHu_Ai_1l2Zp54aKAABvJxmb88"

$supplierIds = @(
    "7a86ae04-16c1-4821-a8ac-6452e2a9e16c",
    "ca9ce785-4722-4ae7-b225-bfeafa9a0c80",
    "2759ef94-9c25-4a48-a473-03bf9e56185c",
    "23fac528-9dd3-4179-adf0-86b4de60cf9a",
    "7567b45f-8317-4dee-b49e-b8f7ed5e4fc0",
    "1251d863-7cfd-4974-a766-ada5fa64a7c4",
    "43485ac9-8ad0-4984-b921-e4ba4df40f0d",
    "389492b4-5821-4ca3-8e74-95e8b5ac7979",
    "b40d39ed-ad0a-417c-88e3-b4288ada888f",
    "b0968bfe-bd75-48bc-89b1-464da73b600a",
    "a443ad8e-4353-453a-9151-c37e0dd7f086",
    "214aef84-5197-4987-81ed-916251c0be19",
    "90de1c15-b3c1-4fbd-ac86-0973346818a3",
    "9d05c632-f190-4bb6-8384-e5090d8af429",
    "5569b2b0-ca20-40ba-88a7-a06b49c1c698",
    "b7b8b1e7-dca5-442f-be44-c3aaf5649a34",
    "03f46b01-cd63-4259-b21a-f6dba7a9d454",
    "b93cee23-7747-4917-a10f-65c41ca55047",
    "bdc66b5c-6ef4-469e-8eb9-0b08ecf12185",
    "efb32cde-b43d-4172-858c-0aa395c792fa",
    "709c2298-42a4-4f4b-8798-5d5a3f8de0e6",
    "3e79431d-cf3f-4f07-83f1-2d56e4f2f4e1",
    "6deaa13d-d3bb-4e54-ae89-5f9a1416ee6d",
    "b407a8e1-d498-447b-8a02-d9e4799d68c0",
    "c685dac7-a94a-4688-9bc0-76c47820070d",
    "e0eacf84-90b9-4ac5-89ba-b7cf8d4d5522",
    "973f8cf7-732b-4d4f-9537-1805989978fc",
    "b6dd8f8a-b8a6-48e2-bcda-1e017ebc1c94",
    "8904c85d-8a7a-4f26-9ab4-d09af8af03a0"
)

$categoryIds = @(
    "0da81f0c-7f86-4527-9163-ab4e1bb7bcfd",
    "64fc339f-bb62-45da-9c09-caf0c52c741a",
    "38409624-5aac-40e9-a97e-c62c37376eb2",
    "a6c4aecc-66f3-4f4e-a039-2586f834f77f",
    "7686ef6e-e575-46e9-b3b1-1337b85be85f",
    "7b19e31d-02b0-425a-89e4-d63c3fd087b3",
    "7581f21b-98cb-4d28-81ce-e51952eeafcd",
    "7f589398-0c9e-4f50-8684-655fdab0ce6f",
    "137458ce-2456-45dc-afe7-392a966c97b3",
    "446b310e-72d3-4e66-af84-4d0d428f5de7",
    "7ce23ef8-f2f8-4b30-810f-edb83dc397b3"
)

$productDescriptions = @(
    "Notebook Dell Inspiron 15", "Mouse Logitech MX Master 3", "Teclado Mecânico Razer",
    "Monitor LG 27 polegadas", "Impressora HP LaserJet", "Webcam Logitech C920",
    "Headset Gamer HyperX", "SSD Samsung 1TB", "Pen Drive 64GB",
    "HD Externo 2TB Seagate", "Roteador TP-Link AC1750", "Switch 8 Portas Gigabit",
    "Cadeira Ergonômica", "Mesa para Escritório", "Estante Metálica",
    "Arquivo de Aço 4 Gavetas", "Armário Baixo", "Gaveteiro Móvel",
    "Resma Papel A4", "Caneta Esferográfica Azul", "Lápis HB",
    "Borracha Branca", "Grampeador", "Clips de Papel",
    "Detergente Líquido", "Desinfetante", "Papel Higiênico",
    "Sabonete Líquido", "Toalha de Papel", "Álcool em Gel",
    "Câmera de Segurança IP", "DVR 8 Canais", "Sensor de Presença",
    "Alarme Residencial", "Extintor de Incêndio", "Placa de Sinalização",
    "Furadeira Elétrica", "Parafusadeira", "Chave de Fenda Set",
    "Alicate Universal", "Martelo", "Trena 5m",
    "Óleo para Motor", "Filtro de Óleo", "Bateria Automotiva",
    "Pneu 175/70 R14", "Lâmpada H4", "Fluido de Freio",
    "Cimento Portland", "Areia Média", "Tijolo Cerâmico",
    "Cal Hidratada", "Argamassa", "Telha Cerâmica",
    "Fio Elétrico 2.5mm", "Tomada 10A", "Interruptor Simples",
    "Disjuntor 20A", "Quadro de Distribuição", "Fita Isolante",
    "Cano PVC 100mm", "Joelho 90 graus", "Registro de Gaveta",
    "Torneira de Parede", "Sifão para Pia", "Abraçadeira",
    "Lâmpada LED 12W", "Lustre Pendente", "Spot de Embutir",
    "Refletor LED 50W", "Arandela", "Dimmer",
    "Ar Condicionado Split", "Ventilador de Teto", "Climatizador",
    "Aquecedor Elétrico", "Desumidificador", "Circulador de Ar",
    "Telefone sem Fio", "Central Telefônica", "Fone de Ouvido Bluetooth",
    "Antena Wi-Fi", "Repetidor de Sinal", "Modem 4G",
    "Caixa de Som Bluetooth", "Soundbar", "Home Theater",
    "Microfone Condensador", "Mixer de Áudio", "Cabo P2",
    "Câmera DSLR Canon", "Lente 50mm", "Tripé Profissional",
    "Flash Externo", "Cartão SD 64GB", "Bolsa para Câmera",
    "Impressora Multifuncional", "Toner HP", "Cartucho Epson",
    "Papel Fotográfico", "Scanner de Mesa", "Plotadora A0",
    "Cabo de Rede CAT6", "Patch Panel 24 Portas", "Rack 19 polegadas",
    "Access Point", "Firewall", "Conversor de Mídia",
    "Pen Drive 128GB", "Cartão MicroSD 256GB", "NAS 4 Baias",
    "Servidor Dell PowerEdge", "Disco SAS 600GB", "Tape Drive LTO",
    "Mouse Pad Gamer", "Webcam Full HD", "Hub USB 3.0",
    "Adaptador HDMI", "Cabo DisplayPort", "Docking Station",
    "Licença Windows 11 Pro", "Office 365", "Antivírus Corporativo",
    "CAL Server 2022", "Adobe Creative Cloud", "AutoCAD 2024",
    "Copo Descartável 200ml", "Guardanapo", "Papel Toalha",
    "Saco de Lixo 100L", "Luva Descartável", "Máscara Cirúrgica",
    "Caixa de Papelão", "Fita Adesiva", "Plástico Bolha",
    "Envelope Pardo", "Lacre de Segurança", "Etiqueta Adesiva",
    "Tesoura", "Estilete", "Régua 30cm",
    "Calculadora", "Perfurador de Papel", "Bandeja para Documentos",
    "Quadro Branco", "Pincel para Quadro", "Apagador",
    "Suporte para Monitor", "Luminária de Mesa", "Relógio de Parede",
    "Vaso Decorativo", "Planta Artificial", "Porta Retrato",
    "Mangueira de Jardim", "Regador 10L", "Tesoura de Poda",
    "Vassoura de Jardim", "Rastelo", "Pá Jardinagem",
    "Bola de Futebol", "Rede para Gol", "Cones para Treino",
    "Colchonete Yoga", "Peso 2kg", "Faixa Elástica",
    "Termômetro Digital", "Oxímetro", "Medidor de Pressão",
    "Estetoscópio", "Luva Procedimento", "Álcool 70%",
    "Béquer 500ml", "Tubo de Ensaio", "Pipeta Graduada",
    "Balança de Precisão", "Microscópio", "Centrífuga",
    "Toalha de Mesa", "Pano de Prato", "Tapete"
)

$counter = 1
$successCount = 0
$errorCount = 0

for ($i = 0; $i -lt 150; $i++) {
    $supplierId = $supplierIds[$i % $supplierIds.Count]
    $categoryId = $categoryIds[$i % $categoryIds.Count]
    $description = $productDescriptions[$i % $productDescriptions.Count]

    # Gerar data de aquisição aleatória nos últimos 6 meses
    $daysAgo = Get-Random -Minimum 1 -Maximum 180
    $acquisitionDate = (Get-Date).AddDays(-$daysAgo).ToString("yyyy-MM-ddTHH:mm:ssZ")

    # Gerar valor de aquisição aleatório
    $acquisitionValue = [math]::Round((Get-Random -Minimum 50 -Maximum 5000) + (Get-Random) / 100, 2)

    # Localizações variadas
    $locations = @("Estoque A - Prateleira", "Almoxarifado Central", "Depósito B", "Sala de TI", "Estoque Geral")
    $location = "$($locations[$i % $locations.Count]) $([math]::Floor($i / 10) + 1)"

    $json = @{
        description = "$description - Unidade $counter"
        supplierId = $supplierId
        categoryId = $categoryId
        acquisitionDate = $acquisitionDate
        acquisitionValue = $acquisitionValue
        location = $location
        observations = "Produto cadastrado automaticamente - Lote $(Get-Date -Format 'yyyyMMdd')"
    } | ConvertTo-Json -Compress

    Write-Host "Criando produto $counter : $description..." -ForegroundColor Cyan

    try {
        $headers = @{
            "Authorization" = "Bearer $token"
            "Content-Type" = "application/json"
        }

        $response = Invoke-RestMethod -Uri "http://localhost:5000/api/Products" -Method POST -Headers $headers -Body $json -ErrorAction Stop
        Write-Host "✓ Produto criado! ID: $($response.id)" -ForegroundColor Green
        $successCount++
    }
    catch {
        Write-Host "✗ Erro ao criar produto: $($_.Exception.Message)" -ForegroundColor Red
        $errorCount++
    }

    $counter++
    Start-Sleep -Milliseconds 50
}

Write-Host ""
Write-Host "=====================================" -ForegroundColor Yellow
Write-Host "Processo concluído!" -ForegroundColor Green
Write-Host "Produtos criados com sucesso: $successCount" -ForegroundColor Green
Write-Host "Erros: $errorCount" -ForegroundColor Red
Write-Host "=====================================" -ForegroundColor Yellow
