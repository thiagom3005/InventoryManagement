#!/bin/bash

TOKEN="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2ZjBjMTM3Ni0zMDNkLTQ2ZjYtOTNlMC1kYjE1NjA2MTUzYzgiLCJ1bmlxdWVfbmFtZSI6ImFkbWluIiwiZW1haWwiOiJhZG1pbkBpbnZlbnRvcnkuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkFkbWluaXN0cmFkb3IgZG8gU2lzdGVtYSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwianRpIjoiNmYzZmIxM2QtYTkyYi00YmU0LWE1M2MtYjRkODNjZmE2NzM1IiwiZXhwIjoxNzY2Nzg3MTg0LCJpc3MiOiJJbnZlbnRvcnlNYW5hZ2VtZW50QVBJIiwiYXVkIjoiSW52ZW50b3J5TWFuYWdlbWVudENsaWVudHMifQ.cbjXTMO6-JYkZzcfDTHu_Ai_1l2Zp54aKAABvJxmb88"

# Array de categorias: nome|código|descrição
categories=(
    "Eletrônicos|ELET|Equipamentos e dispositivos eletrônicos"
    "Informática|INFO|Produtos de informática e tecnologia"
    "Móveis|MOVE|Móveis e mobiliário para escritório"
    "Papelaria|PAPE|Material de escritório e papelaria"
    "Limpeza|LIMP|Produtos de limpeza e higiene"
    "Segurança|SEGU|Equipamentos de segurança"
    "Ferramentas|FERR|Ferramentas e equipamentos"
    "Automotivo|AUTO|Peças e acessórios automotivos"
    "Construção|CONS|Materiais de construção"
    "Elétrica|ELET|Materiais elétricos"
    "Hidráulica|HIDR|Materiais hidráulicos"
    "Iluminação|ILUM|Produtos de iluminação"
    "Climatização|CLIM|Equipamentos de climatização"
    "Telecomunicações|TELE|Equipamentos de telecomunicações"
    "Áudio e Vídeo|AUDI|Equipamentos de áudio e vídeo"
    "Fotografia|FOTO|Equipamentos fotográficos"
    "Impressão|IMPR|Equipamentos de impressão"
    "Rede|REDE|Equipamentos de rede"
    "Armazenamento|ARMS|Dispositivos de armazenamento"
    "Periféricos|PERI|Periféricos de computador"
    "Software|SOFT|Licenças de software"
    "Consumíveis|CONS|Materiais consumíveis"
    "Embalagens|EMBA|Materiais de embalagem"
    "Utensílios|UTEN|Utensílios diversos"
    "Decoração|DECO|Itens de decoração"
    "Jardim|JARD|Produtos para jardinagem"
    "Esporte|ESPO|Equipamentos esportivos"
    "Saúde|SAUD|Equipamentos de saúde"
    "Laboratório|LABO|Equipamentos de laboratório"
    "Textil|TEXT|Produtos têxteis"
)

counter=1
for item in "${categories[@]}"; do
    IFS='|' read -r name code description <<< "$item"
    echo "Criando categoria $counter: $name..."

    curl -s -X POST http://localhost:5000/api/Categories \
        -H "Authorization: Bearer $TOKEN" \
        -H "Content-Type: application/json" \
        -d "{\"name\":\"$name\",\"shortcode\":\"$code\",\"description\":\"$description\"}" \
        > /dev/null && echo "✓ Categoria criada!" || echo "✗ Erro"

    counter=$((counter+1))
done

echo ""
echo "Processo concluído! 30 categorias criadas."
