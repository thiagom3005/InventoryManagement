#!/bin/bash

TOKEN="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2ZjBjMTM3Ni0zMDNkLTQ2ZjYtOTNlMC1kYjE1NjA2MTUzYzgiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwiZW1haWwiOiJhZG1pbkBpbnZlbnRvcnkuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFkbWluaXN0cmFkb3IgZG8gU2lzdGVtYSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwianRpIjoiZDZlMmY3ODctMDVlNi00MmJlLTk5YmItYTk0OGJjOTk3NzhjIiwiZXhwIjoxNzY2Nzg1MTMwLCJpc3MiOiJJbnZlbnRvcnlNYW5hZ2VtZW50QVBJIiwiYXVkIjoiSW52ZW50b3J5TWFuYWdlbWVudENsaWVudHMifQ.zQyqiza2YIBjl9XFysA2l0Tjt5XT1OCNzEpj3-foWh0"

names=(
    "Tech Solutions Brasil Ltda|techsolutions"
    "Informatica Total ME|infototal"
    "Eletronicos Modernos SA|eletronicosmodernos"
    "Distribuidora Alpha Ltda|alpha"
    "MegaTech Importadora|megatech"
    "Computadores e Cia|computadorescia"
    "Suprimentos Office Ltda|officesupply"
    "Global Tech Brasil|globaltech"
    "NextGen Tecnologia|nextgen"
    "Smart Devices Ltda|smartdevices"
    "Premium Informatica|premiuminfo"
    "Digital World SA|digitalworld"
    "TechnoPlus Distribuidora|technoplus"
    "Componentes Brasil Ltda|componentesbr"
    "InfoSystem Comercio|infosystem"
    "Hardware Center Ltda|hwcenter"
    "Tecnologia Avancada ME|techavancada"
    "Suprimentos Tech SA|suptech"
    "MicroInfo Distribuidora|microinfo"
    "Eletronica Pro Ltda|eletronicapro"
    "TechMaster Brasil|techmaster"
    "Inovacao Digital Ltda|inovacaodigital"
    "Conecta Tech SA|conecta"
    "Solucoes TI Ltda|solucoesti"
    "Mega Suprimentos|megasup"
    "Tech Express Ltda|techexpress"
    "Importadora Brasil Tech|brtech"
    "Distribuidor Nacional SA|distnacional"
    "TechParts Comercio|techparts"
    "Digital Commerce Ltda|digitalcommerce"
)

counter=1
for item in "${names[@]}"; do
    IFS='|' read -r name email <<< "$item"
    echo "Criando fornecedor $counter: $name..."

    curl -s -X POST http://localhost:5000/api/Suppliers \
        -H "Authorization: Bearer $TOKEN" \
        -H "Content-Type: application/json" \
        -d "{\"name\":\"$name\",\"email\":\"contato@${email}.com.br\",\"currency\":\"BRL\",\"country\":\"Brasil\"}" \
        > /dev/null && echo "✓ Fornecedor criado!" || echo "✗ Erro"

    counter=$((counter+1))
done

echo ""
echo "Processo concluído! 30 fornecedores criados."
