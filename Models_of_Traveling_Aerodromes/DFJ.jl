using JuMP, GLPK
using Dates

dt1=now(Dates.UTC)

function is_tsp_solved(m, x, numOfMust, visited)
    n = size(x)[1]
    x_val = JuMP.value.(x)

    # find cycle
    sub_route = Int[]
    push!(sub_route, 1)  #make sure that the first point is the startPoint.

    while true
        v, idx = findmax(x_val[sub_route[end],1:n])  #return the max value and its index
        if v == 0.0  #prevent the endless loop incurrd by the mechanism of findmax method
            break
        end
        if length(sub_route) >= numOfMust  #if the number of visited cities are enough, break
            break
        else
            push!(sub_route,idx)  #else put it in the sub route list
        end
    end

    #when the number of visited cities are not enough
    #add a constraint to disallow the current route
    if length(sub_route) < numOfMust
        @constraint(m, sum(x[sub_route,sub_route]) <= length(sub_route)-2) 
        return false                                                       
    end
    return true
end

function readData()
    arr = Array{Int, 2}(undef, 0, 0)
    if isfile("instances/instance_30_1.txt")
        myFile = open("instances/instance_30_1.txt")
        data = readlines(myFile)
        n = length(data)
        arr = fill(0,(n - 3, n -10))

        i = 0
        for line in data
            if line == ""
                continue
            end
            i += 1
            var = split(line, " ")
            for j in 1:length(var)
                arr[i, j] = parse(Int, var[j])
            end
        end
    end
    return arr
end

function square(n::Int64)
    return n*n
end

function solve_tsp(d::Array{Int, 2})
    numOfAero = d[1,1]#number of aerodromes
    start = d[2,1]
    endP = d[3,1]
    numOfMust = d[4,1]#number of points must be visited
    numOfRegion = d[5,1]#number of regions
    maxDistance = d[7,1]#max distance travel without landing

    tempstart = start
    tempend = endP
    
    lines = size(d, 1)#return the number of lines in array d
    
    #let startPoint at the first, lastPoint at the last
    for j in 1:2
        temp = d[start+7,j] #temp is used to store the value needed to be swapped
        d[start+7,j] = d[8,j]
        d[8,j] = temp
        temp = d[endP+7,j]
        d[endP+7,j] = d[numOfAero+7,j]
        d[numOfAero+7,j] = temp
    end    
    
    n = lines - 7 #n -> the size of the coordinate matrix 
    
    regOfEach = Array{Int}(zeros(n))#an array used to store the region of each aerodrome
    for j in 1:n
        regOfEach[j] = d[6,j]
    end

    #because of the swap between the first point and the end point, the position of the region array need to be changed correspondingly
    temp = regOfEach[1] #temp is used to store the value needed to be swapped
    regOfEach[1] = regOfEach[start]
    regOfEach[start] = temp
    
    temp = regOfEach[numOfAero]
    regOfEach[numOfAero] = regOfEach[endP]
    regOfEach[endP] = temp    
    
    #reset the start and end point 
    start = 1
    endP = numOfAero
    
    disArr = Array{Int, 2}(undef, n, n) #an array used to store the information of the distance between each point
    for i in 1:n
        for j in 1:n
            disArr[i, j] = round(sqrt(square(d[i+7,1]-d[j+7,1]) + square(d[i+7,2]-d[j+7,2])))
        end
    end
    
    m = Model(with_optimizer(GLPK.Optimizer))
    #Variables
    @variable(m, x[i in 1:n, j in 1:n], Bin)
    @variable(m, visited[i in 1:n], Bin) #an extra variable used to check whether a city has been visited or not in binary
    
    #Objective
    @objective(m, Min, sum(x[i,j]*disArr[i,j] for i in 1:n, j in 1:n))
    
    #Constraints for preventing self-loop
    @constraint(m, loopsForbidden[i in 1:n], x[i,i] == 0)
    
    #start from startPoint
    @constraint(m, fromStartOut, sum(x[start, j] for j in 1:n) == 1)
    @constraint(m, fromStartIn, sum(x[i, start] for i in 1:n) == 0)
    @constraint(m, visited[1] == 1)
    
    #end at endPoint
    @constraint(m, toEndOut, sum(x[endP, j] for j in 1:n) == 0)
    @constraint(m, toEndIn, sum(x[i, endP] for i in 1:n) == 1)
    @constraint(m, visited[numOfAero] == 1)
    
    #number of point must be visited
    
    for i=1:n, j=1:n
        #the max distance travel without landing
        @constraint(m, x[i,j]*disArr[i,j] <= maxDistance)
        #prevent inter-connections between each two points
        @constraint(m, x[i,j]+x[j,i] <= 1)
    end
    
    #constraint for middle points
    #each middle point could only have one out-degree
    @constraint(m, noMoreThanOne1[i in 2:n-1], sum(x[i,j] for j in 2:n) <= 1)
    #each middle point could only have one in-degree
    @constraint(m, noMoreThanOne2[j in 2:n-1], sum(x[i,j] for i in 1:n-1) <= 1)
    #build the relation between variable x and visited, and ensure that in-degree == out-degree
    @constraint(m, middlePoint1[i in 2:n-1], sum(x[i,j] for j in 1:n) == visited[i])
    @constraint(m, middlePoint2[j in 2:n-1], sum(x[i,j] for i in 1:n) == visited[j])
    
    #region constraint ensure that each region must be visited once
    @constraint(m, [i in 2:n-1], visited[i] == sum(x[i,j] for j in 1:n))
    @constraint(m, region1[i in 1:numOfRegion], sum(visited[j] for j in 1:n if regOfEach[j] == i) >= 1)

    optimize!(m)
    while !is_tsp_solved(m, x, numOfMust, visited)
        optimize!(m)
    end
    
    for i in 1:n
        temp = x[i,tempend]
        x[i,tempend] = x[i,endP]
        x[i,endP] = temp
        temp = x[tempend,i]
        x[tempend,i] = x[endP,i]
        x[endP,i] = temp
        
        temp = x[i,tempstart]
        x[i,tempstart] = x[i,start]
        x[i,start] = temp
        temp = x[tempstart,i]
        x[tempstart,i] = x[start,i]
        x[start,i] = temp
    end

    println("Result - Optimal solution found")
    println("")
    println("Obj: ", JuMP.objective_value(m))
    println(" Distance travelled = ", JuMP.objective_value(m))
    print("Edges used in the solutin :")
    for i in 1:n
        for j in 1:n
            valij =  JuMP.value(x[i,j])
            if valij <= 1.000001 && valij >= 0.999999
                print(i, "->", j, " ")
            end
        end
    end
    println()
    
end

D = readData()
solve_tsp(D)

dt2 = now(Dates.UTC)
println(dt2-dt1)
