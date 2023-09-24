shader_type canvas_item;
render_mode unshaded;

uniform sampler2D gradient: source_color;
uniform float gradient_mix_amount = 10.0;

void vertex(){

}

void fragment() {
	vec4 current_color = texture(TEXTURE, UV);
	
	vec3 color111 = vec3(1, 1, 1); // RED
	vec3 color1 = vec3(1, 0, 0); // BLUE
	vec3 color112 = vec3(1, 0.5, 0); // BLUE
	vec3 color12 = vec3(1, 1, 0); // BLUE
	vec3 color122 = vec3(0.5, 1, 0); // BLUE
	vec3 color2 = vec3(0, 1, 0); // GREEN
	vec3 color223 = vec3(0, 1, 0.5); // GREEN
	vec3 color23 = vec3(0, 1, 1); // Yellow
	vec3 color233 = vec3(0, 0.5, 1); // Orange
	vec3 color3 = vec3(0, 0, 1); // RED
	vec3 color333 = vec3(0.4, 0.4, 0.4); // RED
	
	float new_r;
	float new_g;
	float new_b;
	
	
	
	float ratio = SCREEN_UV.x;
	float time = sin(PI*TIME);
	
	if (ratio>=0.0f+time && ratio<=0.05f+time){
		new_r = color111.r;
		new_g = color111.g;
		new_b = color111.b;
	}
	else if(ratio>0.0f && ratio<=0.11f){
		new_r = color112.r;
		new_g = color112.g;
		new_b = color112.b;
	}
	else if(ratio>0.11f && ratio<=0.23f){
		new_r = color12.r;
		new_g = color12.g;
		new_b = color12.b;
	}
	else if(ratio>0.23f && ratio<=0.34f){
		new_r = color122.r;
		new_g = color122.g;
		new_b = color122.b;
	}
	else if(ratio>0.34f && ratio<=0.45f){
		new_r = color2.r;
		new_g = color2.g;
		new_b = color2.b;
	}
	else if(ratio>0.45f && ratio<=0.56f){
		new_r = color223.r;
		new_g = color223.g;
		new_b = color223.b;
	}
	else if(ratio>0.56f && ratio<=0.67f){
		new_r = color23.r;
		new_g = color23.g;
		new_b = color23.b;
	}
	else if(ratio>0.67f && ratio<=0.78f){
		new_r = color233.r;
		new_g = color233.g;
		new_b = color233.b;
	}
	else if(ratio>0.78f && ratio<=0.89f){
		new_r = color3.r;
		new_g = color3.g;
		new_b = color3.b;
	}
	else if(ratio>0.89f && ratio<=1.0f){
		new_r = color111.r;
		new_g = color111.g;
		new_b = color111.b;
	}
	
	COLOR = vec4(new_b,new_g,new_r,current_color.a);

}
