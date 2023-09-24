shader_type canvas_item;
render_mode unshaded;

uniform sampler2D gradient: source_color;
uniform float gradient_mix_amount = 2.0;

void vertex(){

}

void fragment() {
	vec4 current_color = texture(TEXTURE, UV);
	
	vec3 color1 = vec3(1, 0, 0); // BLUE
	vec3 color112 = vec3(1, 0.5, 0); // BLUE
	vec3 color12 = vec3(1, 1, 0); // BLUE
	vec3 color122 = vec3(0.5, 1, 0); // BLUE
	vec3 color2 = vec3(0, 1, 0); // GREEN
	vec3 color223 = vec3(0, 1, 0.5); // GREEN
	vec3 color23 = vec3(0, 1, 1); // Yellow
	vec3 color233 = vec3(0, 0.5, 1); // Orange
	vec3 color3 = vec3(0, 0, 1); // RED
	
	float new_r;
	float new_g;
	float new_b;
	
	float ratio = SCREEN_UV.x;
	float iratio = 1.0 - SCREEN_UV.x;
	
	if (ratio>=0.0f && ratio<=0.11f){
		new_r = color1.r;
		new_g = color1.g;
		new_b = color1.b;
	}
	else if(ratio>0.11f && ratio<=0.22f){
		new_r = color112.r;
		new_g = color112.g;
		new_b = color112.b;
	}
	else if(ratio>0.22f && ratio<=0.33f){
		new_r = color12.r;
		new_g = color12.g;
		new_b = color12.b;
	}
	else if(ratio>0.33f && ratio<=0.44f){
		new_r = color122.r;
		new_g = color122.g;
		new_b = color122.b;
	}
	else if(ratio>0.44f && ratio<=0.55f){
		new_r = color2.r;
		new_g = color2.g;
		new_b = color2.b;
	}
	else if(ratio>0.55f && ratio<=0.66f){
		new_r = color223.r;
		new_g = color223.g;
		new_b = color223.b;
	}
	else if(ratio>0.66f && ratio<=0.77f){
		new_r = color23.r;
		new_g = color23.g;
		new_b = color23.b;
	}
	else if(ratio>0.77f && ratio<=0.88f){
		new_r = color233.r;
		new_g = color233.g;
		new_b = color233.b;
	}
	else if(ratio>0.88f && ratio<=1.0f){
		new_r = color3.r;
		new_g = color3.g;
		new_b = color3.b;
	}
	
	COLOR = vec4(new_b,new_g,new_r,current_color.a);

}
