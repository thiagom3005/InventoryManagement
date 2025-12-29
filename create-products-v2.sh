#!/bin/bash

TOKEN="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2ZjBjMTM3Ni0zMDNkLTQ2ZjYtOTNlMC1kYjE1NjA2MTUzYzgiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwiZW1haWwiOiJhZG1pbkBpbnZlbnRvcnkuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFkbWluaXN0cmFkb3IgZG8gU2lzdGVtYSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwianRpIjoiNmYzZmIxM2QtYTkyYi00YmU0LWE1M2MtYjRkODNjZmE2NzM1IiwiZXhwIjoxNzY2Nzg3MTg0LCJpc3MiOiJJbnZlbnRvcnlNYW5hZ2VtZW50QVBJIiwiYXVkIjoiSW52ZW50b3J5TWFuYWdlbWVudENsaWVudHMifQ.cbjXTMO6-JYkZzcfDTHu_Ai_1l2Zp54aKAABvJxmb88"

SUPPLIERS=("7a86ae04-16c1-4821-a8ac-6452e2a9e16c" "ca9ce785-4722-4ae7-b225-bfeafa9a0c80" "2759ef94-9c25-4a48-a473-03bf9e56185c" "23fac528-9dd3-4179-adf0-86b4de60cf9a" "7567b45f-8317-4dee-b49e-b8f7ed5e4fc0" "1251d863-7cfd-4974-a766-ada5fa64a7c4" "43485ac9-8ad0-4984-b921-e4ba4df40f0d" "389492b4-5821-4ca3-8e74-95e8b5ac7979" "b40d39ed-ad0a-417c-88e3-b4288ada888f" "b0968bfe-bd75-48bc-89b1-464da73b600a")

CATEGORIES=("0da81f0c-7f86-4527-9163-ab4e1bb7bcfd" "64fc339f-bb62-45da-9c09-caf0c52c741a" "38409624-5aac-40e9-a97e-c62c37376eb2" "a6c4aecc-66f3-4f4e-a039-2586f834f77f" "7686ef6e-e575-46e9-b3b1-1337b85be85f" "7b19e31d-02b0-425a-89e4-d63c3fd087b3" "7581f21b-98cb-4d28-81ce-e51952eeafcd" "7f589398-0c9e-4f50-8684-655fdab0ce6f" "137458ce-2456-45dc-afe7-392a966c97b3" "446b310e-72d3-4e66-af84-4d0d428f5de7")

PRODUCTS=("Notebook Dell" "Mouse Logitech" "Teclado Mecanico" "Monitor LG" "Impressora HP" "Webcam Logitech" "Headset Gamer" "SSD Samsung" "Pen Drive" "HD Externo" "Roteador TP-Link" "Switch Gigabit" "Cadeira Ergonomica" "Mesa Escritorio" "Estante Metalica" "Arquivo Aco" "Armario" "Gaveteiro" "Papel A4" "Caneta" "Lapis" "Borracha" "Grampeador" "Clips" "Detergente" "Desinfetante" "Papel Higienico" "Sabonete" "Toalha Papel" "Alcool Gel" "Camera Seguranca" "DVR 8 Canais" "Sensor Presenca" "Alarme" "Extintor" "Placa Sinalizacao" "Furadeira" "Parafusadeira" "Chave Fenda" "Alicate" "Martelo" "Trena" "Oleo Motor" "Filtro Oleo" "Bateria Auto" "Pneu" "Lampada H4" "Fluido Freio" "Cimento" "Areia")

counter=1
success=0
errors=0

echo "Iniciando criacao de 150 produtos..."

for i in {1..150}; do
    supplier_idx=$((i % 10))
    category_idx=$((i % 10))
    product_idx=$((i % 50))

    supplier="${SUPPLIERS[$supplier_idx]}"
    category="${CATEGORIES[$category_idx]}"
    product="${PRODUCTS[$product_idx]}"

    cost=$((500 + RANDOM % 4500))
    cost_usd=$(awk "BEGIN {print $cost/5.5}")

    echo -n "Criando produto $counter: $product... "

    response=$(curl -s -o /dev/null -w "%{http_code}" -X POST http://localhost:5000/api/Products \
        -H "Authorization: Bearer $TOKEN" \
        -H "Content-Type: application/json" \
        -d "{\"description\":\"$product - Unidade $counter\",\"supplierId\":\"$supplier\",\"categoryId\":\"$category\",\"acquisitionCost\":$cost,\"acquisitionCurrency\":\"BRL\",\"acquisitionCostUsd\":$cost_usd}")

    if [ "$response" = "201" ]; then
        echo "✓"
        ((success++))
    else
        echo "✗ (HTTP $response)"
        ((errors++))
    fi

    ((counter++))
done

echo ""
echo "======================================"
echo "Processo concluido!"
echo "Produtos criados: $success"
echo "Erros: $errors"
echo "======================================"
