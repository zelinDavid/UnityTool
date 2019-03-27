

local Arr = {23, -99, 18, -4, 2, 1, 23, 43, 9, -9, 6, 888, 23, -99, 18}
local len = #Arr

  divid = function (start, endIndex, tab)
    if start == endIndex then  
        return {tab[start]}
    end 

    local mid = math.ceil((start+endIndex)/2)
    local lowArr = divid(start, mid - 1, tab)
    local highArr = divid(mid, endIndex, tab)
 
    for i=1,#highArr do
        local value = highArr[i]
        for j,v in ipairs(lowArr) do
            if value <= v then 
                table.insert(lowArr,j ,value)                
                break 
            end 
            if j == #lowArr then 
                table.insert(lowArr,j + 1,value)
                break
            end 
        end
    end 
    return lowArr
end

for i=1,100000 do
    ret =  divid(1,len, Arr)
end
 