using JuMP
using Cbc
using Dates

dt1=now(Dates.UTC)
println(dt1)

function readData()
    arr = Array{Int, 2}(undef, 0,0 )
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

#Import data
function generate_aerodrones(array_of_aerodrone, map_limit)

    aerodrones = []
    for aerodrone_counter in 1:array_of_aerodrone[1,1]
        push!(aerodrones,
            Dict(
                "id" => aerodrone_counter,
                "x" => array_of_aerodrone[aerodrone_counter+7,1],
                "y" => array_of_aerodrone[aerodrone_counter+7,2],
                "region" => array_of_aerodrone[6,aerodrone_counter]
            )
        )
    end
    println("Cities Generated:", size(aerodrones)[1])
    return aerodrones
end

function calculate_distance_between_two_points(point1, point2)
    return sqrt((((point2[1] - point1[1]))^2) + (((point2[2] - point1[2]))^2))
end

#Calculate traversal paths (measure fitness)
function calculate_chromosome_travel_distance(chromosome)
    travel_distance = 0
    for geneId in 1:length(chromosome) - 1
        point1 = (
            aerodrones[chromosome[geneId]]["x"],
            aerodrones[chromosome[geneId]]["y"]
            )
        point2 = (
            aerodrones[chromosome[geneId + 1]]["x"],
            aerodrones[chromosome[geneId + 1]]["y"]
            )
        travel_distance += calculate_distance_between_two_points(point1, point2)
    end
    return travel_distance
end

#Determine whether the limit length is exceeded
function is_transcend_limit_distance(point1,point2)
    travel_distance = calculate_distance_between_two_points(point1, point2)
    if(travel_distance>array_of_aerodrone[7][1])
        return true
    else
        return false
    end
end

# We shuffle the chromosome here
function shuffle_chromosome(chromosome)
    for i in 1:size(chromosome)[1]
        random_point = rand(1:array_of_aerodrone[1,1]-2, 1)[1]
        chromosome[i], chromosome[random_point] = chromosome[random_point], chromosome[i]
    end
    return chromosome
end

#Generates a specified number of random chromosomes
function generate_initial_population(initial_population_size)
    chromosomes = []
    for population_counter in 1:initial_population_size
        #Cut out the beginning and the end
        initial_chromosome = [1:length(aerodrones);]
        loc_start = findfirst(el -> el == array_of_aerodrone[2,1], initial_chromosome)
        splice!(initial_chromosome, loc_start)
        loc_destination = findfirst(el -> el == array_of_aerodrone[3,1], initial_chromosome)
        splice!(initial_chromosome, loc_destination)
        #Shuffle
        upper_chromosome = rand(array_of_aerodrone[4,1]-2:array_of_aerodrone[1,1]-2, 1)[1]
        chromosome = shuffle_chromosome(initial_chromosome)
        chromosome = chromosome[1:upper_chromosome]
        #Joining together
        chromosome = vcat(array_of_aerodrone[2,1],chromosome,array_of_aerodrone[3,1])
        travel_distance = calculate_chromosome_travel_distance(chromosome)
        push!(chromosomes,
            Dict(
                "id_generation" => 0,
                "id_initial" => population_counter,
                "chromosome" => chromosome,
                "distance" => travel_distance
            )
        )
    end
    return chromosomes
end

#Crossover
function crossover(parent_one_chromosome, parent_two_chromosome, crossover_point)
    offspring_part_one = parent_one_chromosome[1:crossover_point]
    for gene in offspring_part_one
        if gene in parent_two_chromosome
            gene_loc = findfirst(el -> el == gene, parent_two_chromosome)
            splice!(parent_two_chromosome, gene_loc)
        end
    end
    return vcat(offspring_part_one, parent_two_chromosome)
end

#Random point mutation
function mutate(offspring)
    random_mutation_point1 = rand(2:length(offspring)-1)
    random_mutation_point2 = rand(2:length(offspring)-1)
    offspring[random_mutation_point1], offspring[random_mutation_point2] = offspring[random_mutation_point2], offspring[random_mutation_point1]
    return offspring
end

#Determine whether to pass all areas
function is_traverse_all_region(chromosome)
    region_number=array_of_aerodrone[5,1]
    region_array=zeros(Int8,1,region_number)
    for geneID in 1:length(chromosome)
         aerodrone_ID = chromosome[geneID]
         aerodrone_region = array_of_aerodrone[6,aerodrone_ID]
         if(aerodrone_region != 0 && aerodrone_region != region_array[aerodrone_region])
            region_array[aerodrone_region] = aerodrone_region
        end
    end
    for geneID in 1:length(region_array)
        if(region_array[geneID]==0)
            #print("Not traverse all region. ")
            return false
        end
    end
    return true
end

function is_route_transcended(chromosome)
    for geneId in 1:length(chromosome) - 1
        point1 = (
            aerodrones[chromosome[geneId]]["x"],
            aerodrones[chromosome[geneId]]["y"]
            )
        point2 = (
            aerodrones[chromosome[geneId + 1]]["x"],
            aerodrones[chromosome[geneId + 1]]["y"]
            )
        if is_transcend_limit_distance(point2,point1)
            #print("Transcend limit distance.")
            return true
        end
    end
    return false
end

#evolution
function evolve(generation_count, offsprings_count, crossover_point)
    println("Start generating:")
    for generation in 1:generation_count

        for offspring_count in 1:offsprings_count
            #Mating of two chromosomes (crossover + mutation)
            random_parent_one_id = rand(1:size(chromosomes)[1])
            random_parent_two_id = rand(1:size(chromosomes)[1])
            random_parent_one = copy(chromosomes[random_parent_one_id]["chromosome"])
            random_parent_two = copy(chromosomes[random_parent_two_id]["chromosome"])
            offspring = crossover(random_parent_one, random_parent_two, crossover_point)
            offspring = mutate(offspring)
            #Ensure that new offspring pass through all regions
            while(!is_traverse_all_region(offspring) || is_route_transcended(offspring))
                #println()
                #println("illegal route, regenerate offspring")
                random_parent_one_id = rand(1:size(chromosomes)[1])
                random_parent_one = copy(chromosomes[random_parent_one_id]["chromosome"])
                new_chromosome = generate_initial_population(1)
                random_parent_two = copy(new_chromosome[1]["chromosome"])
                #println(random_parent_two)
                offspring = crossover(random_parent_one, random_parent_two, crossover_point)
                offspring = mutate(offspring)
            end
            push!(chromosomes,
                Dict(
                    "id_generation" => generation,
                    "id_offspring" => offspring_count,
                    "chromosome" => offspring,
                    "distance" => calculate_chromosome_travel_distance(offspring)
                    )
            )
        end
        sort!(chromosomes, by=x -> x["distance"], rev=false)
        splice!(chromosomes, offsprings_count÷2+1:size(chromosomes)[1])
        println("generation: ", generation, " ", "Best route: ", chromosomes[1]["distance"])
    end
end

# creating 10 cities randomly
generation_count = 120
offsprings_count = 100000
crossover_point = 2
array_of_aerodrone = readData()
aerodrones = generate_aerodrones(array_of_aerodrone, 500)
# generating 10 initial chromosomes
chromosomes = generate_initial_population(offsprings_count)

# we are running GA for
# 5 generations
# 5 offsprings per generation
# random crossover point as 2

evolve(generation_count, offsprings_count, crossover_point)
println("--------------------------------------------------------")
println("Optimal route:", chromosomes[1]["chromosome"])
println("travel_distance:", chromosomes[1]["distance"])
if(is_route_transcended(chromosomes[1]["chromosome"]))
    println("Not avaliable since transcend the limit distance")
end
if(is_traverse_all_region(chromosomes[1]["chromosome"]))
    println("Congratulations! This route has traversed all the region")
end

print("For drawing: ")
for i in 2:length(chromosomes[1]["chromosome"])
    print(chromosomes[1]["chromosome"][i-1]," ",chromosomes[1]["chromosome"][i]," ")
end
println()

dt2 = now(Dates.UTC)
println(dt2-dt1)
